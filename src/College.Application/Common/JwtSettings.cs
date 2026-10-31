namespace College.Application.Common
{
    public sealed class JwtSettings
    {
        public const string SectionName = "Jwt";

        public string Issuer { get; set; } = "CollegeApi";

        public string Audience { get; set; } = "CollegeApi";

        /// <summary>
        /// Symmetric signing key for HS256 (at least 32 characters). Store via user-secrets or
        /// environment variables; never commit a production key.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        public int AccessTokenExpirationMinutes { get; set; } = 15;

        public int RefreshTokenExpirationDays { get; set; } = 7;
    }
}
