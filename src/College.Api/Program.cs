using College.Api;
using College.Api.Cors;
using College.Api.Middleware;
using College.Api.RateLimiting;
using College.Application;
using College.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructure();

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapApiDocumentation(builder.Configuration);

app.MapDefaultEndpoints();

app.UseMiddleware<CorrelationIdMiddleware>();

// After the correlation id middleware so 429 rejections still carry X-Correlation-Id,
// and before the exception handler so rate limited requests short-circuit without
// touching authentication, HTTPS redirection, or the database.
app.UseConfiguredRateLimiting();
// CORS before the exception handler and HTTPS redirection so ProblemDetails errors and
// redirects also carry CORS headers back to browser clients; preflight OPTIONS requests
// short-circuit here without touching auth or the database.
app.UseConfiguredCors();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();