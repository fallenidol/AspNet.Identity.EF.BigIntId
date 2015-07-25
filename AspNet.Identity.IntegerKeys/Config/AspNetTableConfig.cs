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
                .Set(AspNetIdentityTable.AspNetRoles, "ROLE")
                .Set(AspNetIdentityTable.AspNetUserClaims, "USER_CLAIM")
                .Set(AspNetIdentityTable.AspNetUserLogins, "USER_LOGIN")
                .Set(AspNetIdentityTable.AspNetUserRoles, "USER_ROLE")
                .Set(AspNetIdentityTable.AspNetUsers, "USER");
        }

        public static AspNetTableConfig Pascal()
        {
            return new AspNetTableConfig()
                .Set(AspNetIdentityTable.AspNetRoles, "Role")
                .Set(AspNetIdentityTable.AspNetUserClaims, "UserClaim")
                .Set(AspNetIdentityTable.AspNetUserLogins, "UserLogin")
                .Set(AspNetIdentityTable.AspNetUserRoles, "UserRole")
                .Set(AspNetIdentityTable.AspNetUsers, "User");
        }

        public virtual AspNetTableConfig Set(AspNetIdentityTable key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateTables.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _alternateTables.Remove(key);
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