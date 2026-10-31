using College.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace College.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<Guid> , IEntity
    {
    }
}
