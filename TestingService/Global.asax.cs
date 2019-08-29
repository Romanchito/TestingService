using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using TestingService.App_Start;
using TestingService.BLL.Infrastructure;
using TestingService.Util;

namespace TestingService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new DbInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectModule userModule = new CreateModule();
            NinjectModule serviceModule = new ServiceModule("ConnectionString");
            var kernel = new StandardKernel(userModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            AutoMapperConfig.MapperRegister();
        }
    }
}