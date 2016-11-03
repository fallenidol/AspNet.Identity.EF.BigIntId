using Microsoft.AspNet.Identity;

namespace AspNet.Identity.IntegerKeys
{
    public class EmailTokenProviderWithIntKey<TUser> : EmailTokenProvider<TUser, int> where TUser : class, IUser<int>
    {
    }
}