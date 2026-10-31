using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using College.Application.Common;
using College.Application.Common.Interfaces;
using College.Application.Common.Responses;
using College.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace College.Infrastructure.Auth
{
    public sealed class JwtTokenService(IOptions<JwtSettings> options, UserManager<AppUser> userManager)
        : ITokenService
    {
        private readonly JwtSettings _settings = options.Value;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<AuthTokenPair> GenerateTokenPairAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var now = DateTime.UtcNow;
            var accessExpiresAt = now.AddMinutes(_settings.AccessTokenExpirationMinutes);
            var refreshExpiresAt = now.AddDays(_settings.RefreshTokenExpirationDays);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("fullName", user.FullName),
            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: accessExpiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
                .TrimEnd('=').Replace('+', '-').Replace('/', '_');

            return new AuthTokenPair(accessToken, accessExpiresAt, refreshToken, refreshExpiresAt);
        }

        public string HashRefreshToken(string refreshToken)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
        }
    }
}
