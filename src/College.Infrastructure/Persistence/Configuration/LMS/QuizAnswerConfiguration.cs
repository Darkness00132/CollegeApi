using College.Domain.Entities.LMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.LMS
{
    public sealed class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAnswer> builder)
        {
            builder.HasKey(e => new { e.QuizAttemptId, e.QuestionId });
        }
    }
}
