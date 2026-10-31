using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.Attendence
{
    public class LectureSession : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid ProfessorId { get; set; }
        public ProfessorProfile Professor { get; set; } = null!;

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
