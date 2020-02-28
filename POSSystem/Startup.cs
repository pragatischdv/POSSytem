using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POSSystem.Startup))]
namespace POSSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
