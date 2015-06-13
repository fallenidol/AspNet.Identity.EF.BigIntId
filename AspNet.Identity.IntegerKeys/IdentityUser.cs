using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityUser :
        IdentityUser<int, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
    }
}