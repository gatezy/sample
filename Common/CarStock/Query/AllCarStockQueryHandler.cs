using System.Collections.Generic;
using CarStockService.Entity;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Query
{
    public class AllCarStockQuery:IQuery
    {
        
    }

    internal class AllCarStockQueryHandler : IQueryHandler<AllCarStockQuery,IList<CarStock>>
    {
        private readonly ICarStockRepository _repository;

        public AllCarStockQueryHandler(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public IList<CarStock> Handle(AllCarStockQuery query)
        {
            return _repository.All();
        }
    }
}
