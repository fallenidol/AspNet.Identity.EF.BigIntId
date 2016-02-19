using AspNet.Identity.IntegerKeys.Config;

namespace AspNet.Identity.IntegerKeys.Test
{
    public class IdentityContext : IdentityContextWithIntKey
    {
        public IdentityContext(string connection = "DefaultConnection1")
            : base(connection,
                "identity",
                AspNetTableConfig.AllCapsWithUnderscores(),
                AspNetRolesConfig.AllCapsWithUnderscores(),
                AspNetUserClaimsConfig.AllCapsWithUnderscores(),
                AspNetUserLoginsConfig.AllCapsWithUnderscores(),
                AspNetUserRolesConfig.AllCapsWithUnderscores(),
                AspNetUsersConfig.AllCapsWithUnderscores()
                )
        {
            //
        }
    }
}