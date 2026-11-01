namespace College.Api.Cors;

/// <summary>
/// Strongly-typed options for the "Cors" configuration section. All client types are
/// handled through configuration: browser frontends via their http(s) origins, hybrid
/// mobile apps (WebViews, Capacitor/Ionic) via their custom-scheme origins, and native
/// mobile apps need no CORS at all because they never send an Origin header.
/// </summary>
public sealed class CorsSettings
{
    public const string SectionName = "Cors";

    /// <summary>
    /// Name of the policy applied globally by <see cref="CorsExtensions.UseConfiguredCors"/>.
    /// </summary>
    public string DefaultPolicyName { get; set; } = "Default";

    /// <summary>
    /// Named CORS policies, e.g. one for browser frontends and one for mobile WebViews.
    /// Additional policies can be applied per-endpoint with [EnableCors("Name")].
    /// </summary>
    public Dictionary<string, CorsPolicySettings> Policies { get; set; } = new();
}

/// <summary>
/// Definition of a single named CORS policy.
/// </summary>
public sealed class CorsPolicySettings
{
    /// <summary>
    /// Exact origins to allow (scheme + host, no trailing slash), e.g. "https://app.college.edu".
    /// Hybrid mobile shells use custom schemes such as "capacitor://localhost" or
    /// "ionic://localhost". Use "*" to allow any origin (incompatible with AllowCredentials).
    /// </summary>
    public string[] AllowedOrigins { get; set; } = [];

    /// <summary>
    /// Treat origins like "https://*.college.edu" as prefix wildcards matching any
    /// subdomain. Ignored when <see cref="AllowedOrigins"/> contains "*".
    /// </summary>
    public bool AllowWildcardSubdomains { get; set; }

    /// <summary>
    /// Methods answered on preflight requests; "*" or empty allows any method.
    /// </summary>
    public string[]? AllowedMethods { get; set; }

    /// <summary>
    /// Request headers answered on preflight requests; "*" or empty allows any header.
    /// </summary>
    public string[]? AllowedHeaders { get; set; }

    /// <summary>
    /// Response headers browser clients may read; empty exposes only the correlation id.
    /// </summary>
    public string[]? ExposedHeaders { get; set; }

    /// <summary>
    /// Send Access-Control-Allow-Credentials. Requires explicit origins (never "*").
    /// Keep false for JWT Bearer APIs — tokens travel in the Authorization header,
    /// not in cookies.
    /// </summary>
    public bool AllowCredentials { get; set; }

    /// <summary>
    /// How long browsers may cache preflight answers, cutting down OPTIONS round-trips.
    /// </summary>
    public int PreflightMaxAgeSeconds { get; set; } = 600;
}
