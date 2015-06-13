using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityUserStore<T> : UserStore<T, IdentityRole, int,
        IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where T : IdentityUser
    {
        public IdentityUserStore(DbContext context)
            : base(context)
        {
            //
        }
    }
}