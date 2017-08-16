namespace AspNet.Identity.IntegerKeys
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityRoleStoreWithIntKey : RoleStore<IdentityRoleWithIntKey, int, IdentityUserRoleWithIntKey>
    {
        public IdentityRoleStoreWithIntKey(DbContext context)
            : base(context)
        {
        }
    }
}