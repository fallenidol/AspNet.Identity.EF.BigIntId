using System;
using System.Data.Entity;
using AspNet.Identity.EntityFramework.LongId.Config;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EntityFramework.LongId
{
    public abstract class ApplicationContextBase<T> : IdentityDbContext<T, ApplicationRole,
        long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
        where T : ApplicationUserBase
    {
        #region Constructors

        protected ApplicationContextBase(string nameOrConnectionString,
            AspNetTableConfig tableConfig = null,
            string altSchemaName = null,
            AspNetRolesConfig roleConfig = null,
            AspNetUserClaimsConfig userClaimConfig = null,
            AspNetUserLoginsConfig userLoginConfig = null,
            AspNetUserRolesConfig userRoleConfig = null,
            AspNetUsersConfig userConfig = null
            )
            : base(nameOrConnectionString)
        {
            _userConfig = userConfig ?? new AspNetUsersConfig();
            _roleConfig = roleConfig ?? new AspNetRolesConfig();
            _userLoginConfig = userLoginConfig ?? new AspNetUserLoginsConfig();
            _userClaimConfig = userClaimConfig ?? new AspNetUserClaimsConfig();
            _userRoleConfig = userRoleConfig ?? new AspNetUserRolesConfig();
            _altSchemaName = altSchemaName ?? "dbo";
            _tableConfig = tableConfig ?? new AspNetTableConfig();

            if (string.IsNullOrWhiteSpace(_altSchemaName))
            {
                _altSchemaName = "dbo";
            }
        }

        #endregion Constructors

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roleConfig = modelBuilder.Entity<ApplicationRole>();
            var userClaimConfig = modelBuilder.Entity<ApplicationUserClaim>();
            var userLoginConfig = modelBuilder.Entity<ApplicationUserLogin>();
            var userRoleConfig = modelBuilder.Entity<ApplicationUserRole>();
            var userConfig = modelBuilder.Entity<T>();

            roleConfig.Property(o => o.Id).HasColumnType("BIGINT");
            userClaimConfig.Property(o => o.Id).HasColumnType("BIGINT");
            userClaimConfig.Property(o => o.UserId).HasColumnType("BIGINT");
            userLoginConfig.Property(o => o.UserId).HasColumnType("BIGINT");
            userRoleConfig.Property(o => o.UserId).HasColumnType("BIGINT");
            userRoleConfig.Property(o => o.RoleId).HasColumnType("BIGINT");
            userConfig.Property(o => o.Id).HasColumnType("BIGINT");

            if (!string.IsNullOrWhiteSpace(_altSchemaName) && _altSchemaName.Equals("dbo", StringComparison.InvariantCultureIgnoreCase))
            {
                roleConfig.ToTable(AspNetIdentityTable.AspNetRoles.ToString(), _altSchemaName);
                userClaimConfig.ToTable(AspNetIdentityTable.AspNetUserClaims.ToString(), _altSchemaName);
                userLoginConfig.ToTable(AspNetIdentityTable.AspNetUserLogins.ToString(), _altSchemaName);
                userRoleConfig.ToTable(AspNetIdentityTable.AspNetUserRoles.ToString(), _altSchemaName);
                userConfig.ToTable(AspNetIdentityTable.AspNetUsers.ToString(), _altSchemaName);
            }

            #region AspNetRoles

            if (_tableConfig.AltExists(AspNetIdentityTable.AspNetRoles))
            {
                roleConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetRoles), _altSchemaName);
            }

            if (_roleConfig.AltExists(AspNetRolesColumn.Id))
            {
                roleConfig.Property(o => o.Id).HasColumnName(_roleConfig.AltName(AspNetRolesColumn.Id));
            }
            if (_roleConfig.AltExists(AspNetRolesColumn.Name))
            {
                roleConfig.Property(o => o.Name).HasColumnName(_roleConfig.AltName(AspNetRolesColumn.Name));
            }

            #endregion

            #region AspNetUserClaims

            if (_tableConfig.AltExists(AspNetIdentityTable.AspNetUserClaims))
            {
                userClaimConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserClaims), _altSchemaName);
            }

            if (_userClaimConfig.AltExists(AspNetUserClaimsColumn.Id))
            {
                userClaimConfig.Property(o => o.Id).HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.Id));
            }
            if (_userClaimConfig.AltExists(AspNetUserClaimsColumn.UserId))
            {
                userClaimConfig.Property(o => o.UserId).HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.UserId));
            }
            if (_userClaimConfig.AltExists(AspNetUserClaimsColumn.ClaimValue))
            {
                userClaimConfig.Property(o => o.ClaimValue).HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.ClaimValue));
            }
            if (_userClaimConfig.AltExists(AspNetUserClaimsColumn.ClaimType))
            {
                userClaimConfig.Property(o => o.ClaimType).HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.ClaimType));
            }

            #endregion

            #region AspNetUserLogins

            if (_tableConfig.AltExists(AspNetIdentityTable.AspNetUserLogins))
            {
                userLoginConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserLogins), _altSchemaName);
            }

            if (_userLoginConfig.AltExists(AspNetUserLoginsColumn.UserId))
            {
                userLoginConfig.Property(o => o.UserId).HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.UserId));
            }
            if (_userLoginConfig.AltExists(AspNetUserLoginsColumn.ProviderKey))
            {
                userLoginConfig.Property(o => o.ProviderKey).HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.ProviderKey));
            }
            if (_userLoginConfig.AltExists(AspNetUserLoginsColumn.LoginProvider))
            {
                userLoginConfig.Property(o => o.LoginProvider).HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.LoginProvider));
            }

            #endregion

            #region AspNetUserRoles

            if (_tableConfig.AltExists(AspNetIdentityTable.AspNetUserRoles))
            {
                userRoleConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserRoles), _altSchemaName);
            }

            if (_userRoleConfig.AltExists(AspNetUserRolesColumn.RoleId))
            {
                userRoleConfig.Property(o => o.RoleId).HasColumnName(_userRoleConfig.AltName(AspNetUserRolesColumn.RoleId));
            }
            if (_userRoleConfig.AltExists(AspNetUserRolesColumn.UserId))
            {
                userRoleConfig.Property(o => o.UserId).HasColumnName(_userRoleConfig.AltName(AspNetUserRolesColumn.UserId));
            }

            #endregion

            #region AspNetUsers

            if (_tableConfig.AltExists(AspNetIdentityTable.AspNetUsers))
            {
                userConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUsers), _altSchemaName);
            }

            if (_userConfig.AltExists(AspNetUsersColumn.Id))
            {
                userConfig.Property(o => o.Id).HasColumnName(_userConfig.AltName(AspNetUsersColumn.Id));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.AccessFailedCount))
            {
                userConfig.Property(o => o.AccessFailedCount).HasColumnName(_userConfig.AltName(AspNetUsersColumn.AccessFailedCount));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.Email))
            {
                userConfig.Property(o => o.Email).HasColumnName(_userConfig.AltName(AspNetUsersColumn.Email));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.EmailConfirmed))
            {
                userConfig.Property(o => o.EmailConfirmed).HasColumnName(_userConfig.AltName(AspNetUsersColumn.EmailConfirmed));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.LockoutEnabled))
            {
                userConfig.Property(o => o.LockoutEnabled).HasColumnName(_userConfig.AltName(AspNetUsersColumn.LockoutEnabled));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.LockoutEndDateUtc))
            {
                userConfig.Property(o => o.LockoutEndDateUtc).HasColumnName(_userConfig.AltName(AspNetUsersColumn.LockoutEndDateUtc));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.PasswordHash))
            {
                userConfig.Property(o => o.PasswordHash).HasColumnName(_userConfig.AltName(AspNetUsersColumn.PasswordHash));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.PhoneNumber))
            {
                userConfig.Property(o => o.PhoneNumber).HasColumnName(_userConfig.AltName(AspNetUsersColumn.PhoneNumber));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.PhoneNumberConfirmed))
            {
                userConfig.Property(o => o.PhoneNumberConfirmed).HasColumnName(_userConfig.AltName(AspNetUsersColumn.PhoneNumberConfirmed));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.SecurityStamp))
            {
                userConfig.Property(o => o.SecurityStamp).HasColumnName(_userConfig.AltName(AspNetUsersColumn.SecurityStamp));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.TwoFactorEnabled))
            {
                userConfig.Property(o => o.TwoFactorEnabled).HasColumnName(_userConfig.AltName(AspNetUsersColumn.TwoFactorEnabled));
            }
            if (_userConfig.AltExists(AspNetUsersColumn.UserName))
            {
                userConfig.Property(o => o.UserName).HasColumnName(_userConfig.AltName(AspNetUsersColumn.UserName));
            }

            #endregion
        }

        #endregion Methods

        #region Fields

        private readonly string _altSchemaName;
        private readonly AspNetRolesConfig _roleConfig;
        private readonly AspNetTableConfig _tableConfig;
        private readonly AspNetUserClaimsConfig _userClaimConfig;
        private readonly AspNetUsersConfig _userConfig;
        private readonly AspNetUserLoginsConfig _userLoginConfig;
        private readonly AspNetUserRolesConfig _userRoleConfig;

        #endregion Fields
    }
}