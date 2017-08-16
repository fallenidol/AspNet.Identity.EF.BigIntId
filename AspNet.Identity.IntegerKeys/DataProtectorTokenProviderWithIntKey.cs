namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;

    public class DataProtectorTokenProviderWithIntKey<TUser> : DataProtectorTokenProvider<TUser, int>
        where TUser : class, IUser<int>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="protector" />
        public DataProtectorTokenProviderWithIntKey(IDataProtector protector)
            : base(protector)
        {
        }
    }
}