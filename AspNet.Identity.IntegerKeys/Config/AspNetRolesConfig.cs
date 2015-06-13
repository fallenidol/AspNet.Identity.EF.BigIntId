using System;
using System.Collections.Generic;
using System.Linq;
using AspNet.Identity.IntegerKey;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetRolesConfig
    {
        private readonly Dictionary<AspNetRolesColumn, string> _alternateColumns =
            new Dictionary<AspNetRolesColumn, string>();

        public static AspNetRolesConfig AllCapsWithUnderscores()
        {
            return new AspNetRolesConfig()
                .Add(AspNetRolesColumn.Id, "ROLE_ID")
                .Add(AspNetRolesColumn.Name, "ROLE_NAME");
        }

        public static AspNetRolesConfig Pascal()
        {
            return new AspNetRolesConfig()
                .Add(AspNetRolesColumn.Id, "Id")
                .Add(AspNetRolesColumn.Name, "Name");
        }

        public virtual AspNetRolesConfig Add(AspNetRolesColumn key, string alternateName)
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

        public bool AltExists(AspNetRolesColumn key)
        {
            return _alternateColumns.Keys.Any(t => t == key);
        }

        public string AltName(AspNetRolesColumn key)
        {
            return _alternateColumns[key];
        }
    }
}