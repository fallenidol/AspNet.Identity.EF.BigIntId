using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AspNet.Identity.IntegerKeys.Config;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.IntegerKeys
{
    public class IdentityContextWithIntKey : IdentityContextWithIntKey<IdentityUserWithIntKey>
    {
        public IdentityContextWithIntKey()
        {
        }

        public IdentityContextWithIntKey(string nameOrConnectionString = "DefaultConnection")
            : base(nameOrConnectionString)
        {
        }

        public IdentityContextWithIntKey(
            string nameOrConnectionString = "DefaultConnection",
            string altSchemaName = null,
            AspNetTableConfig tableConfig = null,
            AspNetRolesConfig roleConfig = null,
            AspNetUserClaimsConfig userClaimConfig = null,
            AspNetUserLoginsConfig userLoginConfig = null,
            AspNetUserRolesConfig userRoleConfig = null,
            AspNetUsersConfig userConfig = null
            )
            : base(nameOrConnectionString,
                altSchemaName,
                tableConfig,
                roleConfig,
                userClaimConfig,
                userLoginConfig,
                userRoleConfig,
                userConfig
                )
        {
        }
    }

    public abstract class IdentityContextWithIntKey<T> :
        IdentityDbContext<T, IdentityRoleWithIntKey, int, IdentityUserLoginWithIntKey, IdentityUserRoleWithIntKey, IdentityUserClaimWithIntKey>
        where T : IdentityUserWithIntKey
    {
        private readonly string _altSchemaName;
        private readonly AspNetRolesConfig _roleConfig;
        private readonly AspNetTableConfig _tableConfig;
        private readonly AspNetUserClaimsConfig _userClaimConfig;
        private readonly AspNetUsersConfig _userConfig;
        private readonly AspNetUserLoginsConfig _userLoginConfig;
        private readonly AspNetUserRolesConfig _userRoleConfig;

        protected IdentityContextWithIntKey()
            : this("DefaultConnection")
        {
        }

        protected IdentityContextWithIntKey(
            string nameOrConnectionString = "DefaultConnection",
            string altSchemaName = null,
            AspNetTableConfig tableConfig = null,
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

            try
            {
                (this as IObjectContextAdapter).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
            }
            catch
            {
                //
            }

            try
            {
                (this as IObjectContextAdapter).ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
            }
            catch
            {
                //
            }
        }

        private void ObjectContext_SavingChanges(object sender, EventArgs e)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                EnsureDateTimePropertiesAreUtc(entry.Entity);
            }
        }

        private void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            EnsureDateTimePropertiesAreUtc(e.Entity);
        }

        private static void EnsureDateTimePropertiesAreUtc(object entity)
        {
            if (entity != null)
            {
                var properties = entity.GetType()
                    .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType == typeof (DateTime) || x.PropertyType == typeof (DateTime?));

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof (DateTime?))
                    {
                        var dt = (DateTime?) property.GetValue(entity);

                        if (dt.HasValue && dt.Value.Kind == DateTimeKind.Unspecified)
                        {
                            Trace.WriteLine(string.Format("Setting {0} to UTC", property.Name));

                            var v = DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc);
                            property.SetValue(entity, v);
                        }
                        else if (dt.HasValue && dt.Value.Kind == DateTimeKind.Local)
                        {
                            Trace.WriteLine(string.Format("Changing {0} to UTC", property.Name));

                            property.SetValue(entity, dt.Value.ToUniversalTime());
                        }
                    }

                    if (property.PropertyType == typeof (DateTime))
                    {
                        var dt = (DateTime) property.GetValue(entity);

                        if (dt.Kind == DateTimeKind.Unspecified)
                        {
                            Trace.WriteLine(string.Format("Setting {0} to UTC", property.Name));

                            var v = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                            property.SetValue(entity, v);
                        }
                        else if (dt.Kind == DateTimeKind.Local)
                        {
                            Trace.WriteLine(string.Format("Changing {0} to UTC", property.Name));

                            property.SetValue(entity, dt.ToUniversalTime());
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roleConfig = modelBuilder.Entity<IdentityRoleWithIntKey>();
            var userClaimConfig = modelBuilder.Entity<IdentityUserClaimWithIntKey>();
            var userLoginConfig = modelBuilder.Entity<IdentityUserLoginWithIntKey>();
            var userRoleConfig = modelBuilder.Entity<IdentityUserRoleWithIntKey>();
            var userConfig = modelBuilder.Entity<T>();

            roleConfig.Property(o => o.Id).HasColumnType("INT");
            userClaimConfig.Property(o => o.Id).HasColumnType("INT");
            userClaimConfig.Property(o => o.UserId).HasColumnType("INT");
            userLoginConfig.Property(o => o.UserId).HasColumnType("INT");
            userRoleConfig.Property(o => o.UserId).HasColumnType("INT");
            userRoleConfig.Property(o => o.RoleId).HasColumnType("INT");

            userConfig.Property(o => o.Id).HasColumnType("INT");
            userConfig.Property(o => o.PhoneNumber).HasMaxLength(25);
            userConfig.Property(o => o.SecurityStamp).HasMaxLength(40);
            userConfig.Property(o => o.PasswordHash).HasMaxLength(128);

            if (!string.IsNullOrWhiteSpace(_altSchemaName))
            {
                roleConfig.ToTable(AspNetIdentityTable.AspNetRoles.ToString(), _altSchemaName);
                userClaimConfig.ToTable(AspNetIdentityTable.AspNetUserClaims.ToString(), _altSchemaName);
                userLoginConfig.ToTable(AspNetIdentityTable.AspNetUserLogins.ToString(), _altSchemaName);
                userRoleConfig.ToTable(AspNetIdentityTable.AspNetUserRoles.ToString(), _altSchemaName);
                userConfig.ToTable(AspNetIdentityTable.AspNetUsers.ToString(), _altSchemaName);
            }

            #region AspNetRoles

            if (_tableConfig != null && _tableConfig.AltExists(AspNetIdentityTable.AspNetRoles))
            {
                roleConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetRoles), _altSchemaName);
            }

            if (_roleConfig != null && _roleConfig.AltExists(AspNetRolesColumn.Id))
            {
                roleConfig.Property(o => o.Id).HasColumnName(_roleConfig.AltName(AspNetRolesColumn.Id));
            }
            if (_roleConfig != null && _roleConfig.AltExists(AspNetRolesColumn.Name))
            {
                roleConfig.Property(o => o.Name).HasColumnName(_roleConfig.AltName(AspNetRolesColumn.Name));
            }

            #endregion

            #region AspNetUserClaims

            if (_tableConfig != null && _tableConfig.AltExists(AspNetIdentityTable.AspNetUserClaims))
            {
                userClaimConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserClaims), _altSchemaName);
            }

            if (_userClaimConfig != null && _userClaimConfig.AltExists(AspNetUserClaimsColumn.Id))
            {
                userClaimConfig.Property(o => o.Id).HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.Id));
            }
            if (_userClaimConfig != null && _userClaimConfig.AltExists(AspNetUserClaimsColumn.UserId))
            {
                userClaimConfig.Property(o => o.UserId)
                    .HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.UserId));
            }
            if (_userClaimConfig != null && _userClaimConfig.AltExists(AspNetUserClaimsColumn.ClaimValue))
            {
                userClaimConfig.Property(o => o.ClaimValue)
                    .HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.ClaimValue));
            }
            if (_userClaimConfig != null && _userClaimConfig.AltExists(AspNetUserClaimsColumn.ClaimType))
            {
                userClaimConfig.Property(o => o.ClaimType)
                    .HasColumnName(_userClaimConfig.AltName(AspNetUserClaimsColumn.ClaimType));
            }

            #endregion

            #region AspNetUserLogins

            if (_tableConfig != null && _tableConfig.AltExists(AspNetIdentityTable.AspNetUserLogins))
            {
                userLoginConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserLogins), _altSchemaName);
            }

            if (_userLoginConfig != null && _userLoginConfig.AltExists(AspNetUserLoginsColumn.UserId))
            {
                userLoginConfig.Property(o => o.UserId)
                    .HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.UserId));
            }
            if (_userLoginConfig != null && _userLoginConfig.AltExists(AspNetUserLoginsColumn.ProviderKey))
            {
                userLoginConfig.Property(o => o.ProviderKey)
                    .HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.ProviderKey));
            }
            if (_userLoginConfig != null && _userLoginConfig.AltExists(AspNetUserLoginsColumn.LoginProvider))
            {
                userLoginConfig.Property(o => o.LoginProvider)
                    .HasColumnName(_userLoginConfig.AltName(AspNetUserLoginsColumn.LoginProvider));
            }

            #endregion

            #region AspNetUserRoles

            if (_tableConfig != null && _tableConfig.AltExists(AspNetIdentityTable.AspNetUserRoles))
            {
                userRoleConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUserRoles), _altSchemaName);
            }

            if (_userRoleConfig != null && _userRoleConfig.AltExists(AspNetUserRolesColumn.RoleId))
            {
                userRoleConfig.Property(o => o.RoleId)
                    .HasColumnName(_userRoleConfig.AltName(AspNetUserRolesColumn.RoleId));
            }
            if (_userRoleConfig != null && _userRoleConfig.AltExists(AspNetUserRolesColumn.UserId))
            {
                userRoleConfig.Property(o => o.UserId)
                    .HasColumnName(_userRoleConfig.AltName(AspNetUserRolesColumn.UserId));
            }

            #endregion

            #region AspNetUsers

            if (_tableConfig != null && _tableConfig.AltExists(AspNetIdentityTable.AspNetUsers))
            {
                userConfig.ToTable(_tableConfig.AltName(AspNetIdentityTable.AspNetUsers), _altSchemaName);
            }

            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.Id))
            {
                userConfig.Property(o => o.Id).HasColumnName(_userConfig.AltName(AspNetUsersColumn.Id));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.AccessFailedCount))
            {
                userConfig.Property(o => o.AccessFailedCount)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.AccessFailedCount));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.Email))
            {
                userConfig.Property(o => o.Email).HasColumnName(_userConfig.AltName(AspNetUsersColumn.Email));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.EmailConfirmed))
            {
                userConfig.Property(o => o.EmailConfirmed)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.EmailConfirmed));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.LockoutEnabled))
            {
                userConfig.Property(o => o.LockoutEnabled)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.LockoutEnabled));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.LockoutEndDateUtc))
            {
                userConfig.Property(o => o.LockoutEndDateUtc)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.LockoutEndDateUtc));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.PasswordHash))
            {
                userConfig.Property(o => o.PasswordHash)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.PasswordHash));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.PhoneNumber))
            {
                userConfig.Property(o => o.PhoneNumber)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.PhoneNumber));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.PhoneNumberConfirmed))
            {
                userConfig.Property(o => o.PhoneNumberConfirmed)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.PhoneNumberConfirmed));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.SecurityStamp))
            {
                userConfig.Property(o => o.SecurityStamp)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.SecurityStamp));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.TwoFactorEnabled))
            {
                userConfig.Property(o => o.TwoFactorEnabled)
                    .HasColumnName(_userConfig.AltName(AspNetUsersColumn.TwoFactorEnabled));
            }
            if (_userConfig != null && _userConfig.AltExists(AspNetUsersColumn.UserName))
            {
                userConfig.Property(o => o.UserName).HasColumnName(_userConfig.AltName(AspNetUsersColumn.UserName));
            }

            #endregion
        }
    }
}