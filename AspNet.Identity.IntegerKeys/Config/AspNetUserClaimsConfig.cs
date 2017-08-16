namespace AspNet.Identity.IntegerKeys.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AspNetUserClaimsConfig
    {
        private readonly Dictionary<AspNetUserClaimsColumn, string> _alternateColumns =
            new Dictionary<AspNetUserClaimsColumn, string>();

        public static AspNetUserClaimsConfig AllCapsWithUnderscores()
        {
            return new AspNetUserClaimsConfig()
                .Set(AspNetUserClaimsColumn.Id, "CLAIM_ID")
                .Set(AspNetUserClaimsColumn.UserId, "USER_ID")
                .Set(AspNetUserClaimsColumn.ClaimType, "CLAIM_TYPE")
                .Set(AspNetUserClaimsColumn.ClaimValue, "CLAIM_VALUE");
        }

        public static AspNetUserClaimsConfig Pascal()
        {
            return new AspNetUserClaimsConfig()
                .Set(AspNetUserClaimsColumn.Id, "Id")
                .Set(AspNetUserClaimsColumn.UserId, "UserId")
                .Set(AspNetUserClaimsColumn.ClaimType, "ClaimType")
                .Set(AspNetUserClaimsColumn.ClaimValue, "ClaimValue");
        }

        public AspNetUserClaimsConfig Set(AspNetUserClaimsColumn key, string alternateName)
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

        public bool AltExists(AspNetUserClaimsColumn key)
        {
            return _alternateColumns.Keys.Any(t => t == key);
        }

        public string AltName(AspNetUserClaimsColumn key)
        {
            return _alternateColumns[key];
        }
    }
}