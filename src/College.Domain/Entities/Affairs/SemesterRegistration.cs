using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities.Affairs
{
    public class SemesterRegistration : AggregateRoot
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;

        public SemsterRegistrationStatus Status { get; set; }

        public DateTime? SubmittedAt { get; set; }
    }
}
