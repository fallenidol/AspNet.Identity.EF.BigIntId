using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNet.Identity.IntegerKeys.Config
{
    public class AspNetUsersConfig
    {
        private readonly Dictionary<AspNetUsersColumn, string> _alternateColumns =
            new Dictionary<AspNetUsersColumn, string>();

        public static AspNetUsersConfig AllCapsWithUnderscores()
        {
            return new AspNetUsersConfig()
                .Add(AspNetUsersColumn.Id, "USER_ID")
                .Add(AspNetUsersColumn.AccessFailedCount, "LOGIN_FAIL_COUNT")
                .Add(AspNetUsersColumn.Email, "EMAIL")
                .Add(AspNetUsersColumn.EmailConfirmed, "EMAIL_CONFIRMED")
                .Add(AspNetUsersColumn.LockoutEnabled, "LOCKOUT_ENABLED")
                .Add(AspNetUsersColumn.LockoutEndDateUtc, "LOCKOUT_END_DATETIME_UTC")
                .Add(AspNetUsersColumn.PasswordHash, "PASSWORD_HASH")
                .Add(AspNetUsersColumn.PhoneNumber, "PHONE_NUMBER")
                .Add(AspNetUsersColumn.PhoneNumberConfirmed, "PHONE_NUMBER_CONFIRMED")
                .Add(AspNetUsersColumn.SecurityStamp, "SECURITY_STAMP")
                .Add(AspNetUsersColumn.TwoFactorEnabled, "TWO_FACTOR_ENABLED")
                .Add(AspNetUsersColumn.UserName, "USERNAME");
        }

        public static AspNetUsersConfig Pascal()
        {
            return new AspNetUsersConfig()
                .Add(AspNetUsersColumn.Id, "Id")
                .Add(AspNetUsersColumn.AccessFailedCount, "LoginFailedCount")
                .Add(AspNetUsersColumn.Email, "Email")
                .Add(AspNetUsersColumn.EmailConfirmed, "EmailConfirmed")
                .Add(AspNetUsersColumn.LockoutEnabled, "LockoutEnabled")
                .Add(AspNetUsersColumn.LockoutEndDateUtc, "LockoutPeriodEndDateTimeUtc")
                .Add(AspNetUsersColumn.PasswordHash, "HashedPassword")
                .Add(AspNetUsersColumn.PhoneNumber, "PhoneNumber")
                .Add(AspNetUsersColumn.PhoneNumberConfirmed, "PhoneNumberConfirmed")
                .Add(AspNetUsersColumn.SecurityStamp, "SecurityStamp")
                .Add(AspNetUsersColumn.TwoFactorEnabled, "TwoFactorLoginEnabled")
                .Add(AspNetUsersColumn.UserName, "Username");
        }

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
    }
}