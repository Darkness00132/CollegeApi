using College.Domain.Common;
using College.Domain.Enums;
using System.Collections.ObjectModel;

namespace College.Domain.Entities.Affairs
{
    public class Facility : AggregateRoot
    {
        public string Name { get; set; } = null!;

        public FacilityType Type { get; set; }

        public int Capacity { get; set; }

        public Guid CollegeId { get; set; }
        public AcademicCollege College { get; set; } = null!;

        public Collection<SemesterCourseSchedule> Schedules { get; set; } = new();
    }
}
