namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity;

    public class EmailTokenProviderWithIntKey<TUser> : EmailTokenProvider<TUser, int> where TUser : class, IUser<int>
    {
    }
}