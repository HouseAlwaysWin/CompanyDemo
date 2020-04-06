using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompanyDemo.Startup))]
namespace CompanyDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
