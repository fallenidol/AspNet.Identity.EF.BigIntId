using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.EF.BigIntId.Config
{
    public class AspNetTableConfig
    {
        #region Fields

        private readonly Dictionary<AspNetIdentityTable, string> _alternateTables =
            new Dictionary<AspNetIdentityTable, string>();

        #endregion Fields

        #region Methods

        public virtual AspNetTableConfig Add(AspNetIdentityTable key, string alternateName)
        {
            if (string.IsNullOrWhiteSpace(alternateName))
            {
                throw new ArgumentNullException("alternateName");
            }
            if (_alternateTables.Values.Any(s => s.Equals(alternateName, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException(string.Format("[{0}] has already been configured.", alternateName));
            }
            _alternateTables.Add(key, alternateName);

            return this;
        }

        public bool AltExists(AspNetIdentityTable key)
        {
            return _alternateTables.Keys.Any(t => t == key);
        }

        public string AltName(AspNetIdentityTable key)
        {
            return _alternateTables[key];
        }

        #endregion Methods
    }
}