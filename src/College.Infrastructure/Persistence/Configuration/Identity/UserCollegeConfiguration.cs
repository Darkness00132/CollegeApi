using College.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Infrastructure.Persistence.Configuration.Identity
{
    public sealed class UserCollegeConfiguration : IEntityTypeConfiguration<UserCollege>
    {
        public void Configure(EntityTypeBuilder<UserCollege> builder)
        {
            builder.HasKey(e => new { e.UserId, e.CollegeId });
        }
    }
}
