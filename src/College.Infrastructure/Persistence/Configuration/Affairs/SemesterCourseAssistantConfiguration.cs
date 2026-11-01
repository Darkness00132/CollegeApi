using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class SemesterCourseAssistantConfiguration : IEntityTypeConfiguration<SemesterCourseAssistant>
    {
        public void Configure(EntityTypeBuilder<SemesterCourseAssistant> builder)
        {
            builder.HasKey(e => new { e.SemesterCourseId, e.AcademicAssistantProfileId });
        }
    }
}
