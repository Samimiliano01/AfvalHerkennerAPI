using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sensoring_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Services

// Add MVC controllers support
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API testing/documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Context with SQL Server connection string
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
}
builder.Services.AddDbContext<LitterDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with EF Core stores and token providers
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<LitterDbContext>()
    .AddDefaultTokenProviders();

// Add Identity API endpoints support
builder.Services.AddIdentityApiEndpoints<IdentityUser>();

// Add Authentication and Authorization services
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ✅ Enable Identity API endpoints like /register, /login, etc.
app.MapGroup("/account").MapIdentityApi<IdentityUser>();

// Optional: Seed default roles and admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    var adminEmail = "testtest@test.com";
    var adminPassword = "test123password";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(user, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

app.Run();