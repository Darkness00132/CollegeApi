using College.Domain.Common;

namespace College.Domain.Entities.Identity
{
    /// <summary>
    /// A hashed refresh token issued for obtaining new access tokens; rotated on every refresh.
    /// </summary>
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public AppUser User { get; set; } = null!;

        /// <summary>SHA-256 hash of the token; the raw value is never stored.</summary>
        public string TokenHash { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }

        public string? ReplacedByTokenHash { get; set; }

        public bool IsActive => RevokedAtUtc is null && DateTime.UtcNow < ExpiresAtUtc;
    }
}
