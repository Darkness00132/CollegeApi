using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.Attendence
{
    /// <summary>
    /// Composite primary key: (LectureSessionId, StudentId).
    /// </summary>
    public class LectureAttendance : IEntity
    {
        public Guid LectureSessionId { get; set; }
        public LectureSession LectureSession { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
