using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
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