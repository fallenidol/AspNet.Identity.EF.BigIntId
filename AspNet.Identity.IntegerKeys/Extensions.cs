using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

public class SecurityStampValidator
{
    public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(
        TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity)
        where TManager : UserManager<TUser, int>
        where TUser : class, IUser<int>
    {
        return Microsoft.AspNet.Identity.Owin.SecurityStampValidator.OnValidateIdentity(validateInterval,
            regenerateIdentity, id => id.GetUserId<int>());
    }
}


public static class Extensions
{
    public static async Task<bool> TwoFactorBrowserRememberedAsync(this IAuthenticationManager manager, int userId)
    {
        if (manager == null)
        {
            throw new ArgumentNullException("manager");
        }

        var result = await manager.AuthenticateAsync("TwoFactorRememberBrowser");

        return result != null && result.Identity != null && result.Identity.GetUserId<int>() == userId;
    }

    public static bool TwoFactorBrowserRemembered(this IAuthenticationManager manager, int userId)
    {
        if (manager == null)
        {
            throw new ArgumentNullException("manager");
        }

        return TwoFactorBrowserRememberedAsync(manager, userId).GetAwaiter().GetResult();
    }


    public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager, string xsrfKey,
        int expectedValue)
    {
        if (manager == null)
        {
            throw new ArgumentNullException("manager");
        }

        return GetExternalLoginInfoAsync(manager, xsrfKey, expectedValue).GetAwaiter().GetResult();
    }

    /// <summary>
    ///     Extracts login info out of an external identity
    /// </summary>
    /// <param name="manager" />
    /// <param name="xsrfKey">key that will be used to find the userId to verify</param>
    /// <param name="expectedValue">
    ///     the value expected to be found using the xsrfKey in the AuthenticationResult.Properties
    ///     dictionary
    /// </param>
    /// <returns />
    public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(this IAuthenticationManager manager,
        string xsrfKey, int expectedValue)
    {
        if (manager == null)
        {
            throw new ArgumentNullException("manager");
        }

        var result = await manager.AuthenticateAsync("ExternalCookie");

        return result == null || result.Properties == null || result.Properties.Dictionary == null ||
               !result.Properties.Dictionary.ContainsKey(xsrfKey) ||
               result.Properties.Dictionary[xsrfKey] != expectedValue.ToString()
            ? null
            : GetExternalLoginInfo(result);
    }

    private static ExternalLoginInfo GetExternalLoginInfo(AuthenticateResult result)
    {
        if (result == null || result.Identity == null)
        {
            return null;
        }
        var first = result.Identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (first == null)
        {
            return null;
        }
        var str = result.Identity.Name;
        if (str != null)
        {
            str = str.Replace(" ", "");
        }
        var firstValue =
            result.Identity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
        return new ExternalLoginInfo
        {
            ExternalIdentity = result.Identity,
            Login = new UserLoginInfo(first.Issuer, first.Value),
            DefaultUserName = str,
            Email = firstValue
        };
    }
}