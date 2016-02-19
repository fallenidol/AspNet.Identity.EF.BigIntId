using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityUserWithIntKey :
        IdentityUser<int, IdentityUserLoginWithIntKey, IdentityUserRoleWithIntKey, IdentityUserClaimWithIntKey>
    {
    }
}