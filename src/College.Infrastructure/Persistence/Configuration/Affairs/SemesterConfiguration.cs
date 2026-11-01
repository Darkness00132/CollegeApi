using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.OwnsOne(p => p.AcademicYear, year =>
            {
                year.Ignore(y => y.EndYear);
                year.Property(y => y.StartYear).HasColumnName("AcademicYear_StartYear");
            });
        }
    }
}
