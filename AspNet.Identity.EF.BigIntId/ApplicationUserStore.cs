using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EF.BigIntId
{
    public class ApplicationUserStore<T> : UserStore<T, ApplicationRole, long,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
        where T : ApplicationUserBase
    {
        #region Constructors

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
            //
        }

        #endregion Constructors
    }
}