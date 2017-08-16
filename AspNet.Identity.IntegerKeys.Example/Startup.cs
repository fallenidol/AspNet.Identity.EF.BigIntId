using AspNet.Identity.IntegerKeys.Example;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace AspNet.Identity.IntegerKeys.Example
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}