using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.OwnsOne(p => p.AdmissionAcademicYear, year =>
            {
                year.Ignore(y => y.EndYear);
                year.Property(y => y.StartYear).HasColumnName("AdmissionAcademicYear_StartYear");
            });

            builder.OwnsOne(p => p.CurrentLevel, level =>
            {
                level.Property(l => l.Value).HasColumnName("CurrentLevel");
            });

            builder.OwnsOne(p => p.Cgpa, gpa =>
            {
                gpa.Property(g => g.Value).HasColumnName("Cgpa_Value");
                gpa.Property(g => g.MaximumValue).HasColumnName("Cgpa_MaximumValue");
            });
        }
    }
}
