using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HardwareStore.Web.Startup))]

namespace HardwareStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}