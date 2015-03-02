using Autofac;

namespace Common.CQRS
{
    public interface IQuery
    {
    }

    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery
    {
        TResponse Handle(TQuery query);
    }

    public interface IQueryDispatcher
    {
        TResponse Request<TQuery, TResponse>(TQuery query)
            where TQuery : IQuery;
    }

    internal class QueryDispatcher : IQueryDispatcher
    {
        private IComponentContext _container;

        public QueryDispatcher(IComponentContext container)
        {
            _container = container;
        }

        public TResponse Request<TQuery, TResponse>(TQuery query) where TQuery : IQuery
        {
            IQueryHandler<TQuery, TResponse> handler;
            handler = _container.Resolve<IQueryHandler<TQuery, TResponse>>();
            return handler.Handle(query);
        }
    }
}
