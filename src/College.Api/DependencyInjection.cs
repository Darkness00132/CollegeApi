using College.Api.Middleware;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace College.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration config)
    {
        var apiVersion = config["Version"] ?? "v1";

        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddOpenApi(apiVersion, options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Components ??= new OpenApiComponents();

                document.Components.SecuritySchemes ??=
                    new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] =
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    };

                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static WebApplication MapApiDocumentation(
        this WebApplication app,
        IConfiguration config)
    {
        var apiVersion = config["Version"] ?? "v1";

        app.MapOpenApi();

        app.MapScalarApiReference(options =>
            options
                .WithTitle("College API")
                .AddDocument(apiVersion));

        return app;
    }
}
