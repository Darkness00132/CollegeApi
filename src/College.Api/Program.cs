using College.Api;
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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();