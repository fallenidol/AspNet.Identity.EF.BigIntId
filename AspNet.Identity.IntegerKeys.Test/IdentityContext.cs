namespace AspNet.Identity.IntegerKeys.Test
{
    using AspNet.Identity.IntegerKeys.Config;

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