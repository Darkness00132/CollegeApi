using College.Api;
using College.Api.Middleware;
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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();