using System;
using CarStockService.Entity;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Query
{
    public class CarStockQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    internal class CarStockQueryHandler : IQueryHandler<CarStockQuery, CarStock>
    {
        private readonly ICarStockRepository _repository;

        public CarStockQueryHandler(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public CarStock Handle(CarStockQuery query)
        {
            return _repository.Get(query.Id);
        }
    }
}
