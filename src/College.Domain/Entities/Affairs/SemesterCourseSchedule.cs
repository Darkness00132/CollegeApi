using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    public class SemesterCourseSchedule : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; } = null!;

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
