using Microsoft.EntityFrameworkCore;
using Sensoring_API.ApiKeyAuth;
using Sensoring_API.Data;
using Sensoring_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Listen on port 8080 on every ip address for azure.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Listen on 0.0.0.0:8080
});

// Api Key Authorization
builder.Services.AddTransient<IApiKeyValidation, AdminKeyValidation>();
builder.Services.AddTransient<IApiKeyValidation, UserKeyValidation>();
builder.Services.AddScoped<AdminKeyAuthFilter>();
builder.Services.AddScoped<UserKeyAuthFilter>();


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

// Map controller routes to endpoints
app.MapControllers();

// Run the Application
app.Run();
