using College.Domain.Common;
using College.Domain.ValueObjects;

namespace College.Domain.Entities.Affairs
{
    public class CollegeProgram : AggregateRoot
    {
        public string Name { get; set; } = null!;

        public AcademicLevel MaximumLevel { get; set; } = null!;

        public double MaximumCgpa { get; set; }

        public AcademicYear BylawYear { get; set; } = null!;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
