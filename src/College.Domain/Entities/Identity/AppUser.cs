using College.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace College.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public string FullName { get; set; } = default!;
        public string NationalId { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
