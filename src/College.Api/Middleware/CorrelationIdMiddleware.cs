using System.Diagnostics;

namespace College.Api.Middleware;

/// <summary>
/// Echoes the current W3C trace id — established by the OpenTelemetry ASP.NET Core
/// instrumentation — back to the caller as X-Correlation-Id. Incoming trace context
/// is honored through the standard traceparent header; requests starting a fresh
/// trace get a newly generated trace id.
/// </summary>
public sealed class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-Id";

    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        var correlationId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        context.Response.Headers[HeaderName] = correlationId;

        return _next(context);
    }
}
