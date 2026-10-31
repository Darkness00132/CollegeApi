using College.Domain.Common;
using College.Domain.Entities.Identity;
using College.Domain.Enums;

namespace College.Domain.Entities.Affairs
{
    public class AcademicAssistantProfile : AggregateRoot
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public AcademicAssistantTitle Title { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
