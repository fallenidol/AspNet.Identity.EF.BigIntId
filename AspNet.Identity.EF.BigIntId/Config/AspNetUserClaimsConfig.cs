using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.EntityFramework.LongId.Config
{
    public class AspNetUserClaimsConfig
    {
        #region Fields

        private readonly Dictionary<AspNetUserClaimsColumn, string> _alternateColumns = new Dictionary<AspNetUserClaimsColumn, string>();

        #endregion Fields

        #region Methods

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

        #endregion Methods
    }
}