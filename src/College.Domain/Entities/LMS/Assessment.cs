using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class Assessment : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string AssessmentType { get; set; } = null!;

        public decimal MaximumScore { get; set; }

        public decimal WeightPercentage { get; set; }
    }
}
