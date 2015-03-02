using Autofac;
using Common.CQRS;

namespace Common.Ioc
{
    public class AutofacBindingsModule : Module
    {
        protected override void Load(ContainerBuilder cb)
        {
            cb.RegisterType<QueryDispatcher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            cb.RegisterType<CommandDispatcher>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
