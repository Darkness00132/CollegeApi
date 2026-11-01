namespace College.Api.RateLimiting;

/// <summary>
/// Strongly-typed options for the "RateLimiting" configuration section. Two limiters
/// are supported: a global one applied to every request, partitioned by user id for
/// authenticated callers and by remote IP for anonymous ones, and a stricter per-IP
/// policy for authentication endpoints, where brute force is the realistic threat.
/// Limiters are in-memory and per API instance — scaling out to multiple instances
/// or moving behind a reverse proxy needs a distributed store and forwarded-header
/// handling respectively.
/// </summary>
public sealed class RateLimitingSettings
{
    public const string SectionName = "RateLimiting";

    /// <summary>
    /// Name of the policy intended for authentication endpoints (login, register,
    /// token refresh). Apply it with [EnableRateLimiting(RateLimitingSettings.AuthPolicyName)]
    /// on a controller or .RequireRateLimiting(RateLimitingSettings.AuthPolicyName) on
    /// minimal API endpoints. The global limiter still applies on top of it.
    /// </summary>
    public const string AuthPolicyName = "auth";

    /// <summary>
    /// Master switch for the rate limiting middleware, so load tests or local
    /// troubleshooting can turn it off without code changes.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Sliding-window limit applied to every request.
    /// </summary>
    public RateLimitPolicySettings Global { get; set; } = new()
    {
        PermitLimit = 100,
        WindowSeconds = 60,
        SegmentsPerWindow = 6
    };

    /// <summary>
    /// Fixed-window limit applied per remote IP to authentication endpoints.
    /// </summary>
    public RateLimitPolicySettings Auth { get; set; } = new()
    {
        PermitLimit = 10,
        WindowSeconds = 60
    };
}

/// <summary>
/// Definition of a single rate limit window. When <see cref="SegmentsPerWindow"/> is
/// null the limit is a fixed window (full refill each window); otherwise it is a
/// sliding window divided into that many segments, which smooths bursts at window
/// boundaries.
/// </summary>
public sealed class RateLimitPolicySettings
{
    /// <summary>
    /// Maximum number of requests allowed inside the window.
    /// </summary>
    public int PermitLimit { get; set; }

    /// <summary>
    /// Length of the window in seconds.
    /// </summary>
    public int WindowSeconds { get; set; }

    /// <summary>
    /// Number of segments the sliding window is divided into. Leave null for a
    /// fixed window. Must be at least 1 when set.
    /// </summary>
    public int? SegmentsPerWindow { get; set; }
}
