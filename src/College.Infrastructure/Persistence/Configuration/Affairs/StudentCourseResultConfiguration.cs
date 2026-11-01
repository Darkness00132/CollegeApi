using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class StudentCourseResultConfiguration : IEntityTypeConfiguration<StudentCourseResult>
    {
        public void Configure(EntityTypeBuilder<StudentCourseResult> builder)
        {
            builder.HasKey(e => new { e.SemesterRegistrationId, e.SemesterCourseId });
        }
    }
}
