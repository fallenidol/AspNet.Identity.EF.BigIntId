using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Identity.EntityFramework.LongId;
using AspNet.Identity.EntityFramework.LongId.Config;

namespace AspNet.Identity.EF.BigIntId.Tests
{
    public class ApplicationContext : ApplicationContextBase<ApplicationUser>
    {
        // Capitalize the table and column names
        public ApplicationContext()
            : base("DefaultConnection",
            new AspNetTableConfig()
                .Add(AspNetIdentityTable.AspNetRoles, "ROLE")
                .Add(AspNetIdentityTable.AspNetUserClaims, "USER_CLAIM")
                .Add(AspNetIdentityTable.AspNetUserLogins, "USER_LOGIN")
                .Add(AspNetIdentityTable.AspNetUserRoles, "USER_ROLE")
                .Add(AspNetIdentityTable.AspNetUsers, "USER"),
            "dbo",
            new AspNetRolesConfig()
                .Add(AspNetRolesColumn.Id, "ROLE_ID")
                .Add(AspNetRolesColumn.Name, "ROLE_NAME"),
            new AspNetUserClaimsConfig()
                .Add(AspNetUserClaimsColumn.Id, "ROLE_ID")
                .Add(AspNetUserClaimsColumn.UserId, "USER_ID")
                .Add(AspNetUserClaimsColumn.ClaimType, "CLAIM_TYPE")
                .Add(AspNetUserClaimsColumn.ClaimValue, "CLAIM_VALUE"),
            new AspNetUserLoginsConfig()
                .Add(AspNetUserLoginsColumn.UserId, "USER_ID")
                .Add(AspNetUserLoginsColumn.LoginProvider, "LOGIN_PROVIDER")
                .Add(AspNetUserLoginsColumn.ProviderKey, "PROVIDER_KEY"),
            new AspNetUserRolesConfig()
                .Add(AspNetUserRolesColumn.RoleId, "ROLE_ID")
                .Add(AspNetUserRolesColumn.UserId, "USER_ID"),
            new AspNetUsersConfig()
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
                .Add(AspNetUsersColumn.UserName, "USERNAME")
                )
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var userConfig = modelBuilder.Entity<ApplicationUser>();

            //userConfig.Property(o => o.Firstname).HasColumnName("")
        }
    }
}
