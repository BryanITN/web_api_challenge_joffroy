using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.WebApi;
using web_api_challenge;
using web_api_challenge.Exceptions;
using web_api_challenge.Repositories.Implementations;
using web_api_challenge.Repositories.Interfaces;
using web_api_challenge.Services.Implementations;
using web_api_challenge.Services.Interfaces;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());
            var container = new UnityContainer();
            container.RegisterType<JoffroyChallengeContext>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IStatusTaskRepository, StatusTaskRepository>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IStatusTaskService, StatusTaskService>();
            container.RegisterType<ITaskService, TaskService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
