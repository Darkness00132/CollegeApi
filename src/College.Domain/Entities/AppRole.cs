using College.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace College.Domain.Entities
{
    public class AppRole : IdentityRole<Guid> , IEntity
    {
    }
}
