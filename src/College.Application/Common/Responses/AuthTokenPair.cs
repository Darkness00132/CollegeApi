namespace College.Application.Common.Responses
{
    /// <summary>
    /// A freshly issued token pair for a user.
    /// </summary>
    public sealed record AuthTokenPair(
        string AccessToken,
        DateTime AccessTokenExpiresAtUtc,
        string RefreshToken,
        DateTime RefreshTokenExpiresAtUtc);
}
