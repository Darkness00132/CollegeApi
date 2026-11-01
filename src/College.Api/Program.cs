using College.Api;
using College.Api.Cors;
using College.Api.Middleware;
using College.Application;
using College.Infrastructure;
using College.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddApi(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Aspire's PostgreSQL container uses a dynamic port and generated password, so the
// connection string isn't known ahead of time — apply migrations at startup, before
// hosted services (role seeding) touch the database.
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}

if(app.Environment.IsDevelopment())
    app.MapApiDocumentation(builder.Configuration);

app.MapDefaultEndpoints();

app.UseMiddleware<CorrelationIdMiddleware>();

// CORS before the exception handler and HTTPS redirection so ProblemDetails errors and
// redirects also carry CORS headers back to browser clients; preflight OPTIONS requests
// short-circuit here without touching auth or the database.
app.UseConfiguredCors();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();