using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kdg_MVC.Startup))]
namespace Kdg_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
