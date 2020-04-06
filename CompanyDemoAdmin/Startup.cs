using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompanyDemoAdmin.Startup))]
namespace CompanyDemoAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
