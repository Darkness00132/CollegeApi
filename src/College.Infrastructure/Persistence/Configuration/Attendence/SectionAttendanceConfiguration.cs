using College.Domain.Entities.Attendence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Attendence
{
    public sealed class SectionAttendanceConfiguration : IEntityTypeConfiguration<SectionAttendance>
    {
        public void Configure(EntityTypeBuilder<SectionAttendance> builder)
        {
            builder.HasKey(e => new { e.SectionSessionId, e.StudentId });
        }
    }
}
