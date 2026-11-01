using System.Diagnostics;
using College.Application.Common.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Middleware;

/// <summary>
/// Last-resort handler for exceptions that escape the pipeline: it logs the exception
/// and writes the unified failure response — an RFC 7807 ProblemDetails shaped like the
/// ones produced by <see cref="Common.ResultExtensions"/> for <see cref="ErrorType.Failure"/>.
/// The current correlation id is carried in the problem extensions; exception internals
/// are never exposed to the client.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private const string InternalErrorCode = "SERVER.INTERNAL_ERROR";
    private const string InternalErrorDescription =
        "An unexpected error occurred while processing the request.";

    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (httpContext.Response.HasStarted)
        {
            _logger.LogError(
                exception,
                "Unhandled exception after the response started for {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);

            return false;
        }

        if (httpContext.RequestAborted.IsCancellationRequested)
        {
            _logger.LogInformation(
                exception,
                "Request aborted by the client: {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);

            return true;
        }

        _logger.LogError(
            exception,
            "Unhandled exception while processing {Method} {Path}",
            httpContext.Request.Method, httpContext.Request.Path);

        var error = Error.Failure(InternalErrorCode, InternalErrorDescription);

        var problem = new ProblemDetails
        {
            Type = "https://college-api.errors/failure",
            Title = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = error.Description,
        };
        problem.Extensions["code"] = error.Code;
        problem.Extensions["errorType"] = error.Type.ToString();

        var correlationId = Activity.Current?.TraceId.ToString();
        if (!string.IsNullOrEmpty(correlationId))
            problem.Extensions["correlationId"] = correlationId;

        httpContext.Response.StatusCode = problem.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}
