using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.Attendence
{
    public class SectionSession : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid AcademicAssistantId { get; set; }
        public AcademicAssistantProfile AcademicAssistant { get; set; } = null!;

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
