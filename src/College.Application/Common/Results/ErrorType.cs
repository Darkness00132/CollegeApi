namespace College.Application.Common.Results
{
    /// <summary>
    /// The category of an error, used to derive the HTTP status code in the API layer.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>An unexpected failure (maps to 500).</summary>
        Failure = 0,

        /// <summary>The request input is invalid (maps to 400).</summary>
        Validation = 1,

        /// <summary>The requested resource does not exist (maps to 404).</summary>
        NotFound = 2,

        /// <summary>Authentication is required or the credentials are invalid (maps to 401).</summary>
        Unauthorized = 3,

        /// <summary>The authenticated user lacks permission (maps to 403).</summary>
        Forbidden = 4,

        /// <summary>The request conflicts with the current state of the resource (maps to 409).</summary>
        Conflict = 5,

        /// <summary>A domain/business rule was violated (maps to 422).</summary>
        BusinessRule = 6,
    }
}
