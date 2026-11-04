using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using PortfolioApi.DataAccess;
using PortfolioApi.Interfaces;
using PortfolioApi.Middleware;
using PortfolioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------- 1) Register services (the tools our app can use) ----------
builder.Services.AddControllers();

// Dependency Injection: when a class asks for an interface, ASP.NET Core supplies the class.
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IContactService, ContactService>();

// Friendly JSON errors for anything unexpected
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// CORS: lets the React app (a different address) call this API from the browser.
// The allowed addresses are listed in appsettings.json -> Cors:AllowedOrigins.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
    options.AddPolicy("ReactApp", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()));

// Rate limit for the contact form (protects your database from spam)
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("contact", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

var app = builder.Build();

// ---------- 2) Middleware pipeline (runs for every request, in this order) ----------
app.UseExceptionHandler();
app.UseCors("ReactApp");
app.UseRateLimiter();
app.MapControllers();

// A friendly home page for the API, so opening http://localhost:5080 shows something useful.
app.MapGet("/", () => Results.Ok(new
{
    name = "Portfolio API",
    status = "running",
    endpoints = new[]
    {
        "GET  /api/portfolio/profile",
        "GET  /api/portfolio/skills",
        "GET  /api/portfolio/projects",
        "GET  /api/portfolio/education",
        "GET  /api/portfolio/certifications",
        "POST /api/contact"
    }
}));

app.Run();
