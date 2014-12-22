using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EF.BigIntId
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
    }
}