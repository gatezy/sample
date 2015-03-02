using System;
using Autofac;

namespace Common.CQRS
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandDispatcher
    {
        void Dispatch<TParameter>(TParameter command) where TParameter : ICommand;
    }

    internal class CommandDispatcher : ICommandDispatcher
    {
        private IComponentContext _container;

        public CommandDispatcher(IComponentContext container)
        {
            _container = container;
        }

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            ICommandHandler<TParameter> handler;
            handler = _container.Resolve<ICommandHandler<TParameter>>();
            handler.Handle(command);
        }
    }
}
