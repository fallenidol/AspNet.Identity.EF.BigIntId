namespace AspNet.Identity.IntegerKeys.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AspNetRolesConfig
    {
        private readonly Dictionary<AspNetRolesColumn, string> _alternateColumns =
            new Dictionary<AspNetRolesColumn, string>();

        public static AspNetRolesConfig AllCapsWithUnderscores()
        {
            return new AspNetRolesConfig()
                .Set(AspNetRolesColumn.Id, "ROLE_ID")
                .Set(AspNetRolesColumn.Name, "ROLE_NAME");
        }

        public static AspNetRolesConfig Pascal()
        {
            return new AspNetRolesConfig()
                .Set(AspNetRolesColumn.Id, "Id")
                .Set(AspNetRolesColumn.Name, "Name");
        }

        public AspNetRolesConfig Set(AspNetRolesColumn key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateColumns.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException(string.Format("[{0}] has already been configured.", alternateName));
            }

            if (_alternateColumns.ContainsKey(key))
            {
                _alternateColumns.Remove(key);
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