using System;
using System.Collections.Generic;
using System.Linq;
using AspNet.Identity.IntegerKey;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetUserClaimsConfig
    {
        private readonly Dictionary<AspNetUserClaimsColumn, string> _alternateColumns =
            new Dictionary<AspNetUserClaimsColumn, string>();

        public static AspNetUserClaimsConfig AllCapsWithUnderscores()
        {
            return new AspNetUserClaimsConfig()
                .Add(AspNetUserClaimsColumn.Id, "CLAIM_ID")
                .Add(AspNetUserClaimsColumn.UserId, "USER_ID")
                .Add(AspNetUserClaimsColumn.ClaimType, "CLAIM_TYPE")
                .Add(AspNetUserClaimsColumn.ClaimValue, "CLAIM_VALUE");
        }

        public static AspNetUserClaimsConfig Pascal()
        {
            return new AspNetUserClaimsConfig()
                .Add(AspNetUserClaimsColumn.Id, "Id")
                .Add(AspNetUserClaimsColumn.UserId, "UserId")
                .Add(AspNetUserClaimsColumn.ClaimType, "ClaimType")
                .Add(AspNetUserClaimsColumn.ClaimValue, "ClaimValue");
        }

        public virtual AspNetUserClaimsConfig Add(AspNetUserClaimsColumn key, string alternateName)
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