using Autofac;
using CarStockService.Command;
using CarStockService.Query;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Ioc
{
    public class AutofacBindingsModule : Module
    {
        protected override void Load(ContainerBuilder cb)
        {
            //explicit binding is better, lazy here...
            cb.RegisterType<CarStockRepository>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<CarStockQueryHandler>().AsImplementedInterfaces();
            cb.RegisterType<AllCarStockQueryHandler>().AsImplementedInterfaces();
            cb.RegisterType<UpdateCarStockCommandHandler>().AsImplementedInterfaces();
            cb.RegisterType<DeleteCarStockCommandHandler>().AsImplementedInterfaces();
            cb.RegisterType<CreateCarStockCommandHandler>().AsImplementedInterfaces();
        }
    }
}
