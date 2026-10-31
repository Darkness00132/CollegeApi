using College.Domain.Common;
using College.Domain.Entities.Affairs;
using College.Domain.Enums;

namespace College.Domain.Entities.Identity
{
    /// <summary>
    /// Junction between AppUser and AcademicCollege.
    /// Composite primary key: (UserId, CollegeId).
    /// </summary>
    public class UserCollege : IEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public Guid CollegeId { get; set; }
        public AcademicCollege College { get; set; } = null!;

        public AffiliationType AffiliationType { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsActive { get; set; }
    }
}
