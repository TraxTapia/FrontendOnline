using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trax.Frontend.Web.Startup))]
namespace Trax.Frontend.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
