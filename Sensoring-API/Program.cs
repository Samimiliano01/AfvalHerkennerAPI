using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sensoring_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Services

// Add MVC controllers support
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API testing/documentation
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
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

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<LitterDbContext>();

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

// ✅ Enable Identity API endpoints like /register, /login, etc.
app.MapGroup("/account").MapIdentityApi<IdentityUser>().AllowAnonymous();

// Run the Application

app.Run();
