using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EF.BigIntId
{
    public abstract class ApplicationUserBase :
        IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
    }
}