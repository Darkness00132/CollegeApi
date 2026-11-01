using College.Domain.Entities.Attendence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Attendence
{
    public sealed class LectureAttendanceConfiguration : IEntityTypeConfiguration<LectureAttendance>
    {
        public void Configure(EntityTypeBuilder<LectureAttendance> builder)
        {
            builder.HasKey(e => new { e.LectureSessionId, e.StudentId });
        }
    }
}
