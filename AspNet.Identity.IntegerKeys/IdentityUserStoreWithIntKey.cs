namespace AspNet.Identity.IntegerKeys
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityUserStoreWithIntKey<T> : UserStore<T, IdentityRoleWithIntKey, int,
        IdentityUserLoginWithIntKey, IdentityUserRoleWithIntKey, IdentityUserClaimWithIntKey>
        where T : IdentityUserWithIntKey
    {
        public IdentityUserStoreWithIntKey(DbContext context)
            : base(context)
        {
            //
        }
    }
}