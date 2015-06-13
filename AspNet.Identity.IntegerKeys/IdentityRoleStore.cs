using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityRoleStore : RoleStore<IdentityRole, int, IdentityUserRole>
    {
        public IdentityRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}