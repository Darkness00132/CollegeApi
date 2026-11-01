using College.Domain.Entities.LMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.LMS
{
    public sealed class AssessmentResultConfiguration : IEntityTypeConfiguration<AssessmentResult>
    {
        public void Configure(EntityTypeBuilder<AssessmentResult> builder)
        {
            builder.HasKey(e => new { e.AssessmentId, e.StudentId });
        }
    }
}
