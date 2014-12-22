using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.Identity.EntityFramework.LongId
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
    }
}