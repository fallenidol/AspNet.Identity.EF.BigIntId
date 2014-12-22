using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EntityFramework.LongId
{
    public abstract class ApplicationUserBase : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
    }
}