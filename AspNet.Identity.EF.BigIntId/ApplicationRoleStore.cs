using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EntityFramework.LongId
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, long, ApplicationUserRole>
    {
        #region Constructors

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}