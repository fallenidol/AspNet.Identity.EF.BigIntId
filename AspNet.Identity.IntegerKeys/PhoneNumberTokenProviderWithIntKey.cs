namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity;

    public class PhoneNumberTokenProviderWithIntKey<TUser> : PhoneNumberTokenProvider<TUser, int>
        where TUser : class, IUser<int>
    {
    }
}