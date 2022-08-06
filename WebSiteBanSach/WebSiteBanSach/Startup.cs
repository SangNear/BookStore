using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteBanSach.Startup))]
namespace WebSiteBanSach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
