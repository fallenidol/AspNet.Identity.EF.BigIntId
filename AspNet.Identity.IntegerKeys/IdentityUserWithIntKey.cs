namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityUserWithIntKey :
        IdentityUser<int, IdentityUserLoginWithIntKey, IdentityUserRoleWithIntKey, IdentityUserClaimWithIntKey>
    {
    }
}