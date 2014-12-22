using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.EF.BigIntId.Config
{
    public class AspNetRolesConfig
    {
        #region Fields

        private readonly Dictionary<AspNetRolesColumn, string> _alternateColumns =
            new Dictionary<AspNetRolesColumn, string>();

        #endregion Fields

        #region Methods

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

        #endregion Methods
    }
}