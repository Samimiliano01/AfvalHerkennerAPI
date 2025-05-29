using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sensoring_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Services

// Add MVC controllers support
builder.Services.AddControllers();

// Add Authentication services (used by Identity)
builder.Services.AddAuthentication();

// Add Swagger/OpenAPI generation (API documentation and testing UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Identity with EF Core stores and token providers
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<LitterDbContext>()
    .AddDefaultTokenProviders();


// Configure Database Context with SQL Server connection string
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
}
builder.Services.AddDbContext<LitterDbContext>(options =>
    options.UseSqlServer(connectionString));

// Build Application

var app = builder.Build();

// Configure Middleware Pipeline

// Enable Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS for security
app.UseHttpsRedirection();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes to endpoints
app.MapControllers();

// Seed Default Roles and Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Create "Admin" role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create "User" role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    // Define admin user credentials (change to your desired email/password)
    var adminEmail = "testtest@test.com";
    var adminPassword = "test123password";

    // Check if admin user exists
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        // Create new admin user
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(user, adminPassword);

        // Assign "Admin" role if user creation succeeded
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

// Run the Application

app.Run();
