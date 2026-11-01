using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class CollegeProgramConfiguration : IEntityTypeConfiguration<CollegeProgram>
    {
        public void Configure(EntityTypeBuilder<CollegeProgram> builder)
        {
            builder.OwnsOne(p => p.MaximumLevel, level =>
            {
                level.Property(l => l.Value).HasColumnName("MaximumLevel");
            });

            builder.OwnsOne(p => p.BylawYear, year =>
            {
                year.Ignore(y => y.EndYear);
                year.Property(y => y.StartYear).HasColumnName("BylawYear_StartYear");
            });
        }
    }
}
