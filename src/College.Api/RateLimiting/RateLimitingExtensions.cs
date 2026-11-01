using System.Security.Claims;
using System.Threading.RateLimiting;
using College.Api.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace College.Api.RateLimiting;

public static class RateLimitingExtensions
{
    // Prefixes keep user-id and IP partitions from colliding, e.g. a user named
    // like an IP address would otherwise share one bucket.
    private const string UserPartitionPrefix = "user:";
    private const string IpPartitionPrefix = "ip:";

    private const string UnknownPartitionKey = "ip:unknown";

    /// <summary>
    /// Registers the rate limiting middleware from the "RateLimiting" configuration
    /// section: a global sliding window over every request and a stricter per-IP
    /// fixed window for authentication endpoints. Fails fast on startup when the
    /// configuration is missing or inconsistent, so a broken setup never reaches
    /// production silently.
    /// </summary>
    public static IServiceCollection AddConfiguredRateLimiting(
        this IServiceCollection services,
        IConfiguration config)
    {
        var section = config.GetSection(RateLimitingSettings.SectionName);
        if (!section.Exists())
        {
            throw new InvalidOperationException(
                $"Rate limiting is not configured: define a '{RateLimitingSettings.SectionName}' section in appsettings.json.");
        }

        var settings = section.Get<RateLimitingSettings>()
            ?? throw new InvalidOperationException(
                $"The '{RateLimitingSettings.SectionName}' section could not be bound to {nameof(RateLimitingSettings)}.");

        Validate(settings);

        services.AddRateLimiter(options =>
        {
            // The middleware defaults to 503; 429 is the status callers are required
            // to honor and the one that carries a Retry-After header.
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // Retry-After is only sent when the limiter reports it: the fixed window
            // (auth policy) does, the sliding window (global) never does — that is a
            // framework limitation of SlidingWindowRateLimiter, not something this
            // handler can fill in.
            options.OnRejected = static async (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                        ((int)Math.Ceiling(retryAfter.TotalSeconds)).ToString();
                }

                await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Too Many Requests",
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.9",
                    Detail = "The client has sent too many requests in a given amount of time."
                }, cancellationToken);
            };

            // Global limiter: authenticated callers are limited by user id, anonymous
            // ones by remote IP. Behind a reverse proxy RemoteIpAddress is the proxy's
            // address unless UseForwardedHeaders is configured first.
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var key = userId is not null
                    ? $"{UserPartitionPrefix}{userId}"
                    : httpContext.Connection.RemoteIpAddress?.ToString() is { } remoteIp
                        ? $"{IpPartitionPrefix}{remoteIp}"
                        : UnknownPartitionKey;

                // A sliding window (SegmentsPerWindow set) smooths bursts across the
                // window; a null value means a plain fixed window instead.
                return settings.Global.SegmentsPerWindow is { } segments
                    ? RateLimitPartition.GetSlidingWindowLimiter(key, _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = settings.Global.PermitLimit,
                        Window = TimeSpan.FromSeconds(settings.Global.WindowSeconds),
                        SegmentsPerWindow = segments,
                        QueueLimit = 0
                    })
                    : RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = settings.Global.PermitLimit,
                        Window = TimeSpan.FromSeconds(settings.Global.WindowSeconds),
                        QueueLimit = 0
                    });
            });

            // Auth policy: per-IP fixed window for anonymous authentication endpoints;
            // per-account abuse is already covered by Identity lockout.
            options.AddPolicy(RateLimitingSettings.AuthPolicyName, httpContext =>
            {
                var key = httpContext.Connection.RemoteIpAddress?.ToString() is { } remoteIp
                    ? $"{IpPartitionPrefix}{remoteIp}"
                    : UnknownPartitionKey;

                return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = settings.Auth.PermitLimit,
                    Window = TimeSpan.FromSeconds(settings.Auth.WindowSeconds),
                    QueueLimit = 0
                });
            });
        });

        services.AddOptions<RateLimitingSettings>().Bind(section);

        return services;
    }

    /// <summary>
    /// Applies rate limiting globally. Must run after CORS so 429 responses carry
    /// CORS headers back to browser clients, and before authentication so rejected
    /// requests never reach the token validation or the database.
    /// </summary>
    public static IApplicationBuilder UseConfiguredRateLimiting(this IApplicationBuilder app)
    {
        var enabled = app.ApplicationServices
            .GetRequiredService<IOptions<RateLimitingSettings>>().Value.Enabled;

        return enabled ? app.UseRateLimiter() : app;
    }

    private static void Validate(RateLimitingSettings settings)
    {
        ValidatePolicy(settings.Global, nameof(settings.Global));
        ValidatePolicy(settings.Auth, nameof(settings.Auth));
    }

    private static void ValidatePolicy(RateLimitPolicySettings policy, string policyName)
    {
        if (policy.PermitLimit <= 0)
        {
            throw new InvalidOperationException(
                $"RateLimiting: '{RateLimitingSettings.SectionName}:{policyName}:{nameof(policy.PermitLimit)}' " +
                $"must be a positive number, but is '{policy.PermitLimit}'.");
        }

        if (policy.WindowSeconds <= 0)
        {
            throw new InvalidOperationException(
                $"RateLimiting: '{RateLimitingSettings.SectionName}:{policyName}:{nameof(policy.WindowSeconds)}' " +
                $"must be a positive number of seconds, but is '{policy.WindowSeconds}'.");
        }

        if (policy.SegmentsPerWindow is < 1)
        {
            throw new InvalidOperationException(
                $"RateLimiting: '{RateLimitingSettings.SectionName}:{policyName}:{nameof(policy.SegmentsPerWindow)}' " +
                $"must be at least 1 when set, but is '{policy.SegmentsPerWindow}'.");
        }
    }
}
