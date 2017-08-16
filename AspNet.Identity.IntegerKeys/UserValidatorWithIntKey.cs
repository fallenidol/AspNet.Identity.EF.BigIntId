namespace AspNet.Identity.IntegerKeys
{
    using Microsoft.AspNet.Identity;

    public class UserValidatorWithIntKey<TUser> : UserValidator<TUser, int> where TUser : class, IUser<int>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="manager"></param>
        public UserValidatorWithIntKey(UserManager<TUser, int> manager)
            : base(manager)
        {
        }
    }
}