using College.Domain.Common;

namespace College.Domain.Entities
{
    public class Department : Entity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public Guid CollegeId { get; set; }
        public AcademicCollege College { get; set; } = null!;
    }
}
