using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetTableConfig
    {
        private readonly Dictionary<AspNetIdentityTable, string> _alternateTables =
            new Dictionary<AspNetIdentityTable, string>();

        public static AspNetTableConfig AllCapsWithUnderscores()
        {
            return new AspNetTableConfig()
                .Add(AspNetIdentityTable.AspNetRoles, "ROLE")
                .Add(AspNetIdentityTable.AspNetUserClaims, "USER_CLAIM")
                .Add(AspNetIdentityTable.AspNetUserLogins, "USER_LOGIN")
                .Add(AspNetIdentityTable.AspNetUserRoles, "USER_ROLE")
                .Add(AspNetIdentityTable.AspNetUsers, "USER");
        }

        public static AspNetTableConfig Pascal()
        {
            return new AspNetTableConfig()
                .Add(AspNetIdentityTable.AspNetRoles, "Role")
                .Add(AspNetIdentityTable.AspNetUserClaims, "UserClaim")
                .Add(AspNetIdentityTable.AspNetUserLogins, "UserLogin")
                .Add(AspNetIdentityTable.AspNetUserRoles, "UserRole")
                .Add(AspNetIdentityTable.AspNetUsers, "User");
        }

        public virtual AspNetTableConfig Add(AspNetIdentityTable key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateTables.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException(string.Format("[{0}] has already been configured.", alternateName));
            }
            _alternateTables.Add(key, alternateName);

            return this;
        }

        public bool AltExists(AspNetIdentityTable key)
        {
            return _alternateTables.Keys.Any(t => t == key);
        }

        public string AltName(AspNetIdentityTable key)
        {
            return _alternateTables[key];
        }
    }
}