using College.Domain.Entities.LMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.LMS
{
    public sealed class AssignmentSubmissionConfiguration : IEntityTypeConfiguration<AssignmentSubmission>
    {
        public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
        {
            builder.HasKey(e => new { e.AssignmentId, e.StudentId });
        }
    }
}
