using College.Domain.Common;
using College.Domain.Enums;
using College.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace College.Domain.Entities
{
    public class SemesterCourse : AggregateRoot
    {
        public SemesterCourseStatus Status { get; set; }

        public AcademicLevel Level { get; set; } = null!;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;

        public Guid ProfessorId { get; set; }
        public ProfessorProfile Professor { get; set; } = null!;

        public Collection<AcademicAssistantProfile> AcademicAssistants { get; set; } =  [];
    }
}
