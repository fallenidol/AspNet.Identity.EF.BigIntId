using System;
using System.Collections.Generic;
using System.Linq;
using AspNet.Identity.IntegerKey;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetUserLoginsConfig
    {
        private readonly Dictionary<AspNetUserLoginsColumn, string> _alternateColumns =
            new Dictionary<AspNetUserLoginsColumn, string>();

        public static AspNetUserLoginsConfig AllCapsWithUnderscores()
        {
            return new AspNetUserLoginsConfig()
                .Add(AspNetUserLoginsColumn.UserId, "USER_ID")
                .Add(AspNetUserLoginsColumn.LoginProvider, "LOGIN_PROVIDER")
                .Add(AspNetUserLoginsColumn.ProviderKey, "PROVIDER_KEY");
        }

        public static AspNetUserLoginsConfig Pascal()
        {
            return new AspNetUserLoginsConfig()
                .Add(AspNetUserLoginsColumn.UserId, "UserId")
                .Add(AspNetUserLoginsColumn.LoginProvider, "LoginProvider")
                .Add(AspNetUserLoginsColumn.ProviderKey, "ProviderKey");
        }

        public virtual AspNetUserLoginsConfig Add(AspNetUserLoginsColumn key, string alternateName)
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