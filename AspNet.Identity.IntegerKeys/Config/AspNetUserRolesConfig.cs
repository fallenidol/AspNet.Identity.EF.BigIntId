using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetUserRolesConfig
    {
        private readonly Dictionary<AspNetUserRolesColumn, string> _alternateColumns =
            new Dictionary<AspNetUserRolesColumn, string>();

        public static AspNetUserRolesConfig AllCapsWithUnderscores()
        {
            return new AspNetUserRolesConfig()
                .Add(AspNetUserRolesColumn.RoleId, "ROLE_ID")
                .Add(AspNetUserRolesColumn.UserId, "USER_ID");
        }

        public static AspNetUserRolesConfig Pascal()
        {
            return new AspNetUserRolesConfig()
                .Add(AspNetUserRolesColumn.RoleId, "RoleId")
                .Add(AspNetUserRolesColumn.UserId, "UserId");
        }

        public virtual AspNetUserRolesConfig Add(AspNetUserRolesColumn key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateColumns.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException(string.Format("[{0}] has already been configured.", alternateName));
            }
            _alternateColumns.Add(key, alternateName);

            return this;
        }

        public bool AltExists(AspNetUserRolesColumn key)
        {
            return _alternateColumns.Keys.Any(t => t == key);
        }

        public string AltName(AspNetUserRolesColumn key)
        {
            return _alternateColumns[key];
        }
    }
}