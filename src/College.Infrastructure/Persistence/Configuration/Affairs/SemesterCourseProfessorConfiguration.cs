using College.Domain.Entities.Affairs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Affairs
{
    public sealed class SemesterCourseProfessorConfiguration : IEntityTypeConfiguration<SemesterCourseProfessor>
    {
        public void Configure(EntityTypeBuilder<SemesterCourseProfessor> builder)
        {
            builder.HasKey(e => new { e.SemesterCourseId, e.ProfessorId });
        }
    }
}
