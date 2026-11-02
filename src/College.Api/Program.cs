using College.Api;
using College.Api.Cors;
using College.Api.Middleware;
using College.Api.RateLimiting;
using College.Application;
using College.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructure();

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication();

builder.Services.AddSerilog((services, configuration) => configuration
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapApiDocumentation(builder.Configuration);

app.MapDefaultEndpoints();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseConfiguredRateLimiting();

app.UseConfiguredCors();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();