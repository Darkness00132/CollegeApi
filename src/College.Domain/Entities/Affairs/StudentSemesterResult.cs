using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    public class StudentSemesterResult : AggregateRoot
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;

        public decimal SemesterGpa { get; set; }

        public decimal CumulativeGpa { get; set; }

        public int EarnedCreditHours { get; set; }
    }
}
