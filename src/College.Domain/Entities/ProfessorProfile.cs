using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities
{
    public class ProfessorProfile : AggregateRoot
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public AcademicTitle AcademicTitle { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
