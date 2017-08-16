namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityRoleWithIntKey : IdentityRole<int, IdentityUserRoleWithIntKey>
    {
    }
}