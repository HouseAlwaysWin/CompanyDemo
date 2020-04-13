using Autofac;
using Autofac.Integration.Mvc;
using DBAccess;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace CompanyDemoAdmin.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            builder.RegisterControllers(Assembly.GetExecutingAssembly());


            builder.Register(u => new UnitOfWork(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                .As<IUnitOfWork>()
                .SingleInstance();



            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}