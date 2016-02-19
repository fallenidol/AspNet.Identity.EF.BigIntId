using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityRoleStoreWithIntKey : RoleStore<IdentityRoleWithIntKey, int, IdentityUserRoleWithIntKey>
    {
        public IdentityRoleStoreWithIntKey(DbContext context)
            : base(context)
        {
        }
    }
}