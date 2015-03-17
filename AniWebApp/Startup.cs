using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AniWebApp.Startup))]
namespace AniWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
