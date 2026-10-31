using College.Application.Common.Responses;
using College.Domain.Entities.Identity;

namespace College.Application.Common.Interfaces
{
    /// <summary>
    /// Issues signed access/refresh token pairs for JWT authentication.
    /// </summary>
    public interface ITokenService
    {
        Task<AuthTokenPair> GenerateTokenPairAsync(AppUser user);

        /// <summary>
        /// SHA-256 hash used to persist and look up refresh tokens; the raw token is never stored.
        /// </summary>
        string HashRefreshToken(string refreshToken);
    }
}
