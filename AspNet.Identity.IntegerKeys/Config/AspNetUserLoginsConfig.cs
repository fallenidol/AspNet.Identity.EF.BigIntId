using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetUserLoginsConfig
    {
        private readonly Dictionary<AspNetUserLoginsColumn, string> _alternateColumns =
            new Dictionary<AspNetUserLoginsColumn, string>();

        public static AspNetUserLoginsConfig AllCapsWithUnderscores()
        {
            return new AspNetUserLoginsConfig()
                .Set(AspNetUserLoginsColumn.UserId, "USER_ID")
                .Set(AspNetUserLoginsColumn.LoginProvider, "LOGIN_PROVIDER")
                .Set(AspNetUserLoginsColumn.ProviderKey, "PROVIDER_KEY");
        }

        public static AspNetUserLoginsConfig Pascal()
        {
            return new AspNetUserLoginsConfig()
                .Set(AspNetUserLoginsColumn.UserId, "UserId")
                .Set(AspNetUserLoginsColumn.LoginProvider, "LoginProvider")
                .Set(AspNetUserLoginsColumn.ProviderKey, "ProviderKey");
        }

        public AspNetUserLoginsConfig Set(AspNetUserLoginsColumn key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateColumns.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _alternateColumns.Remove(key);
            }
            _alternateColumns.Add(key, alternateName);

            return this;
        }

        public bool AltExists(AspNetUserLoginsColumn key)
        {
            return _alternateColumns.Keys.Any(t => t == key);
        }

        public string AltName(AspNetUserLoginsColumn key)
        {
            return _alternateColumns[key];
        }
    }
}