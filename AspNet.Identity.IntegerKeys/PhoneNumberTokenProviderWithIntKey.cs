using Microsoft.AspNet.Identity;

namespace AspNet.Identity.IntegerKeys
{
    public class PhoneNumberTokenProviderWithIntKey<TUser> : PhoneNumberTokenProvider<TUser, int> where TUser : class, IUser<int>
    {
    }
}