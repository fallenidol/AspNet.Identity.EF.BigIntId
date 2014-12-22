using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EntityFramework.LongId
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