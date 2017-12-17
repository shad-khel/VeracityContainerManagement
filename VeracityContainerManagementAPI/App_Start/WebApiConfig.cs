using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using VeracityContainerManagementAPI.DB;
using SimpleInjector.Lifestyles;
using System.Net.Http;
using VeracityContainerManagementAPI.Helpers;

namespace VeracityContainerManagementAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register <IDataModel, DataModel>(Lifestyle.Scoped);
            container.Register(()=> new HttpClient(), Lifestyle.Scoped);
            container.Register<IHttpClientHelperClass, HttpClientHelperClass>(Lifestyle.Scoped);
            container.Register<IVeracityResourceSharingHelper, VeracityResourceSharingHelper>();
            container.Register<IVeracityUserHelper, VeracityUserHelper>();
            container.Register<IVeracityContainerHelper, VeracityContainerHelper>();
            
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
