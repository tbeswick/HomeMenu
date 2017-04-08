using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeMenu.Startup))]
namespace HomeMenu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
