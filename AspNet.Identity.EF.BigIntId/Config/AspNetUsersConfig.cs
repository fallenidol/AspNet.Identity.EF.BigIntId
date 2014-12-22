using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.EntityFramework.LongId.Config
{
    public class AspNetUsersConfig
    {
        #region Fields

        private readonly Dictionary<AspNetUsersColumn, string> _alternateColumns = new Dictionary<AspNetUsersColumn, string>();

        #endregion Fields

        #region Methods

        public virtual AspNetUsersConfig Add(AspNetUsersColumn key, string alternateName)
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

        public bool AltExists(AspNetUsersColumn key)
        {
            return _alternateColumns.Keys.Any(t => t == key);
        }

        public string AltName(AspNetUsersColumn key)
        {
            return _alternateColumns[key];
        }

        #endregion Methods
    }
}