using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sensoring_API.ApiKeyAuth;
using Sensoring_API.Data;
using Sensoring_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register Services

// Api Key Authorization
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddHttpContextAccessor();

// Add MVC controllers support
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API testing/documentation
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ILitterRepository, LitterRepository>();
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
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LitterDbContext>()
    .AddDefaultTokenProviders();

// builder.Services.Configure<IdentityOptions>(options =>
// {
//     options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
// });

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
