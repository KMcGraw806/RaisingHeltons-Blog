using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaisingHeltons.Startup))]
namespace RaisingHeltons
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
