using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi.App_Start
{
    public static class AutofacBindings
    {
        public static void Initialize()
        {
            ContainerBuilder cb = new ContainerBuilder();

            cb.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Bind(cb);
            IContainer container = cb.Build();
            
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void Bind(ContainerBuilder cb)
        {
            cb.RegisterModule(new Common.Ioc.AutofacBindingsModule());
            cb.RegisterModule(new CarStockService.Ioc.AutofacBindingsModule());
        }
    }
}