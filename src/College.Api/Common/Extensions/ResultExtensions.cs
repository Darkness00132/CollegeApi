using System.Diagnostics;
using System.Text.RegularExpressions;
using College.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Common;

/// <summary>
/// Maps application results onto HTTP responses. Failures always produce an
/// RFC 7807 ProblemDetails body whose status code is derived from <see cref="ErrorType"/>,
/// with the error's code, type and the current correlation id carried in the problem
/// extensions. Successes can use the default mapping or a custom action per endpoint.
/// </summary>
public static class ResultExtensions
{
    private static readonly Dictionary<ErrorType, int> StatusCodeByErrorType = new()
    {
        [ErrorType.Failure] = StatusCodes.Status500InternalServerError,
        [ErrorType.Validation] = StatusCodes.Status400BadRequest,
        [ErrorType.NotFound] = StatusCodes.Status404NotFound,
        [ErrorType.Unauthorized] = StatusCodes.Status401Unauthorized,
        [ErrorType.Forbidden] = StatusCodes.Status403Forbidden,
        [ErrorType.Conflict] = StatusCodes.Status409Conflict,
        [ErrorType.BusinessRule] = StatusCodes.Status422UnprocessableEntity,
    };

    /// <summary>
    /// Converts a non-generic result: success becomes 204 No Content, failure becomes
    /// a ProblemDetails response.
    /// </summary>
    public static IActionResult ToActionResult(this Result result) =>
        result.Match<IActionResult>(
            onSuccess: () => new NoContentResult(),
            onFailure: ToProblem);

    /// <summary>
    /// Converts a typed result: success becomes 200 OK with the value, failure becomes
    /// a ProblemDetails response.
    /// </summary>
    public static IActionResult ToActionResult<T>(this Result<T> result) =>
        result.Match<IActionResult>(
            onSuccess: value => new OkObjectResult(value),
            onFailure: ToProblem);

    /// <summary>
    /// Converts a typed result with a custom success response (e.g. 201 Created or 204 No
    /// Content); failure always goes through the unified ProblemDetails mapping.
    /// </summary>
    public static IActionResult ToActionResult<T>(
        this Result<T> result,
        Func<T, IActionResult> onSuccess) =>
        result.Match(onSuccess, ToProblem);

    private static ObjectResult ToProblem(Error error)
    {
        var statusCode = StatusCodeByErrorType.TryGetValue(error.Type, out var code)
            ? code
            : StatusCodes.Status500InternalServerError;

        var problem = new ProblemDetails
        {
            Type = $"https://college-api.errors/{ToSlug(error.Type)}",
            Title = GetTitle(error.Type),
            Status = statusCode,
            Detail = error.Description,
        };
        problem.Extensions["code"] = error.Code;
        problem.Extensions["errorType"] = error.Type.ToString();

        var correlationId = Activity.Current?.TraceId.ToString();
        if (!string.IsNullOrEmpty(correlationId))
            problem.Extensions["correlationId"] = correlationId;

        return new ObjectResult(problem) { StatusCode = statusCode };
    }

    private static string GetTitle(ErrorType type) => type switch
    {
        ErrorType.Failure => "An unexpected error occurred.",
        ErrorType.Validation => "The request is invalid.",
        ErrorType.NotFound => "The requested resource was not found.",
        ErrorType.Unauthorized => "Authentication is required.",
        ErrorType.Forbidden => "Access to this resource is forbidden.",
        ErrorType.Conflict => "The request conflicts with the current state.",
        ErrorType.BusinessRule => "A business rule was violated.",
        _ => "An error occurred.",
    };

    private static string ToSlug(ErrorType type) =>
        Regex.Replace(type.ToString(), "(?<!^)([A-Z])", "-$1").ToLowerInvariant();
}
