using College.Api.Cors;
using College.Api.Middleware;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace College.Api.Cors;

public static class CorsExtensions
{
    private static readonly string[] DefaultAllowedMethods =
        ["GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS", "HEAD"];

    // Authorization/Content-Type cover JWT requests, traceparent/tracestate cover the
    // W3C trace context the API honors for correlation.
    private static readonly string[] DefaultAllowedHeaders =
        ["Authorization", "Content-Type", "Accept", "traceparent", "tracestate", "X-Correlation-Id"];

    private static readonly string[] DefaultExposedHeaders = [CorrelationIdMiddleware.HeaderName];

    /// <summary>
    /// Registers named CORS policies from the "Cors" configuration section and fails
    /// fast on startup when the configuration is missing or inconsistent, so a broken
    /// setup never reaches production silently.
    /// </summary>
    public static IServiceCollection AddConfiguredCors(
        this IServiceCollection services,
        IConfiguration config)
    {
        var section = config.GetSection(CorsSettings.SectionName);
        if (!section.Exists())
        {
            throw new InvalidOperationException(
                $"CORS is not configured: define a '{CorsSettings.SectionName}' section in appsettings.json.");
        }

        var settings = section.Get<CorsSettings>()
            ?? throw new InvalidOperationException(
                $"The '{CorsSettings.SectionName}' section could not be bound to {nameof(CorsSettings)}.");

        Validate(settings);

        services.AddCors(options =>
        {
            foreach (var (name, policy) in settings.Policies)
            {
                options.AddPolicy(name, builder => ConfigurePolicy(builder, policy));
            }
        });

        services.AddOptions<CorsSettings>().Bind(section);

        return services;
    }

    /// <summary>
    /// Applies the globally configured default CORS policy. Must run before the
    /// exception handler so error responses also carry CORS headers, and before
    /// authentication/authorization. Native mobile clients are unaffected: requests
    /// without an Origin header bypass CORS entirely.
    /// </summary>
    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        var defaultPolicyName = app.ApplicationServices
            .GetRequiredService<IOptions<CorsSettings>>().Value.DefaultPolicyName;

        return app.UseCors(defaultPolicyName);
    }

    private static void Validate(CorsSettings settings)
    {
        if (settings.Policies.Count == 0)
        {
            throw new InvalidOperationException(
                $"CORS: '{CorsSettings.SectionName}:Policies' must define at least one policy.");
        }

        if (!settings.Policies.ContainsKey(settings.DefaultPolicyName))
        {
            throw new InvalidOperationException(
                $"CORS: '{CorsSettings.SectionName}:DefaultPolicyName' is '{settings.DefaultPolicyName}', " +
                $"but no such policy exists. Defined policies: {string.Join(", ", settings.Policies.Keys)}.");
        }

        foreach (var (name, policy) in settings.Policies)
        {
            if (policy.AllowedOrigins.Length == 0)
            {
                throw new InvalidOperationException(
                    $"CORS: policy '{name}' has no '{nameof(CorsPolicySettings.AllowedOrigins)}'. " +
                    "Native mobile apps need none, but browser and WebView clients do — add the " +
                    "origins for this environment to appsettings.json or appsettings.{Environment}.json.");
            }

            if (policy.AllowCredentials && policy.AllowedOrigins.Contains("*"))
            {
                throw new InvalidOperationException(
                    $"CORS: policy '{name}' cannot combine AllowCredentials with a '*' origin — " +
                    "browsers reject that combination.");
            }

            if (policy.PreflightMaxAgeSeconds <= 0)
            {
                throw new InvalidOperationException(
                    $"CORS: policy '{name}' must set a positive {nameof(CorsPolicySettings.PreflightMaxAgeSeconds)}.");
            }
        }
    }

    private static void ConfigurePolicy(CorsPolicyBuilder builder, CorsPolicySettings policy)
    {
        if (policy.AllowedOrigins.Contains("*"))
        {
            builder.AllowAnyOrigin();
        }
        else
        {
            builder.WithOrigins(
                policy.AllowedOrigins.Select(origin => origin.TrimEnd('/')).ToArray());

            if (policy.AllowWildcardSubdomains)
                builder.SetIsOriginAllowedToAllowWildcardSubdomains();
        }

        if (policy.AllowedMethods is not { Length: > 0 } || policy.AllowedMethods.Contains("*"))
            builder.AllowAnyMethod();
        else
            builder.WithMethods(policy.AllowedMethods);

        if (policy.AllowedHeaders is not { Length: > 0 } || policy.AllowedHeaders.Contains("*"))
            builder.AllowAnyHeader();
        else
            builder.WithHeaders(policy.AllowedHeaders);

        builder.WithExposedHeaders(
            policy.ExposedHeaders is { Length: > 0 } ? policy.ExposedHeaders : DefaultExposedHeaders);

        if (policy.AllowCredentials)
            builder.AllowCredentials();

        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(policy.PreflightMaxAgeSeconds));
    }
}
