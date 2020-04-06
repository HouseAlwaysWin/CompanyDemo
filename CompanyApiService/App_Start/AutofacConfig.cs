using Autofac;
using Autofac.Integration.WebApi;
using CompanyApiService.Services;
using CompanyApiService.Services.Interfaces;
using DBAccess;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace CompanyApiService.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.Register(u => new UnitOfWork(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                .As<IUnitOfWork>()
                .SingleInstance();

            builder.RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeService>()
               .As<IEmployeeService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
               .As<IProductService>()
               .InstancePerLifetimeScope();



            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}