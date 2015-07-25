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
                .Set(AspNetUsersColumn.Id, "USER_ID")
                .Set(AspNetUsersColumn.AccessFailedCount, "LOGIN_FAIL_COUNT")
                .Set(AspNetUsersColumn.Email, "EMAIL")
                .Set(AspNetUsersColumn.EmailConfirmed, "EMAIL_CONFIRMED")
                .Set(AspNetUsersColumn.LockoutEnabled, "LOCKOUT_ENABLED")
                .Set(AspNetUsersColumn.LockoutEndDateUtc, "LOCKOUT_END_DATETIME_UTC")
                .Set(AspNetUsersColumn.PasswordHash, "PASSWORD_HASH")
                .Set(AspNetUsersColumn.PhoneNumber, "PHONE_NUMBER")
                .Set(AspNetUsersColumn.PhoneNumberConfirmed, "PHONE_NUMBER_CONFIRMED")
                .Set(AspNetUsersColumn.SecurityStamp, "SECURITY_STAMP")
                .Set(AspNetUsersColumn.TwoFactorEnabled, "TWO_FACTOR_ENABLED")
                .Set(AspNetUsersColumn.UserName, "USERNAME");
        }

        public static AspNetUsersConfig Pascal()
        {
            return new AspNetUsersConfig()
                .Set(AspNetUsersColumn.Id, "Id")
                .Set(AspNetUsersColumn.AccessFailedCount, "LoginFailedCount")
                .Set(AspNetUsersColumn.Email, "Email")
                .Set(AspNetUsersColumn.EmailConfirmed, "EmailConfirmed")
                .Set(AspNetUsersColumn.LockoutEnabled, "LockoutEnabled")
                .Set(AspNetUsersColumn.LockoutEndDateUtc, "LockoutPeriodEndDateTimeUtc")
                .Set(AspNetUsersColumn.PasswordHash, "HashedPassword")
                .Set(AspNetUsersColumn.PhoneNumber, "PhoneNumber")
                .Set(AspNetUsersColumn.PhoneNumberConfirmed, "PhoneNumberConfirmed")
                .Set(AspNetUsersColumn.SecurityStamp, "SecurityStamp")
                .Set(AspNetUsersColumn.TwoFactorEnabled, "TwoFactorLoginEnabled")
                .Set(AspNetUsersColumn.UserName, "Username");
        }

        public AspNetUsersConfig Set(AspNetUsersColumn key, string alternateName)
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