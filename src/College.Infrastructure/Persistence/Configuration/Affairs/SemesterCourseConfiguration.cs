using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class SemesterCourseConfiguration : IEntityTypeConfiguration<SemesterCourse>
    {
        public void Configure(EntityTypeBuilder<SemesterCourse> builder)
        {
            builder.OwnsOne(p => p.Level, level =>
            {
                level.Property(l => l.Value).HasColumnName("Level");
            });
        }
    }
}
