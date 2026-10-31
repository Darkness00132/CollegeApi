using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.Attendence
{
    /// <summary>
    /// Composite primary key: (SectionSessionId, StudentId).
    /// </summary>
    public class SectionAttendance : IEntity
    {
        public Guid SectionSessionId { get; set; }
        public SectionSession SectionSession { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
