namespace College.Application.Common.Results
{
    /// <summary>
    /// A structured, serializable description of why an operation failed.
    /// </summary>
    public sealed record Error(string Code, string Description, ErrorType Type)
    {
        public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
        public static Error Validation(string code, string description) => new(code, description, ErrorType.Validation);
        public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
        public static Error Unauthorized(string code, string description) => new(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code, string description) => new(code, description, ErrorType.Forbidden);
        public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
        public static Error BusinessRule(string code, string description) => new(code, description, ErrorType.BusinessRule);
    }
}
