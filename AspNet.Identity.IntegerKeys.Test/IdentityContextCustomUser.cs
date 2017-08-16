namespace AspNet.Identity.IntegerKeys.Test
{
    using AspNet.Identity.IntegerKeys.Config;

    public class IdentityContextCustomUser : IdentityContextWithIntKey<CustomUser>
    {
        public IdentityContextCustomUser(string connection = "DefaultConnection2")
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