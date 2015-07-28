using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStockService.Entity;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Command
{
    public class UpdateCarStockCommand : ICommand
    {
        public CarStock Car { get; private set; }

        public UpdateCarStockCommand(Guid id, string make, string model, int year, string color, int stock)
        {
            this.Car = new CarStock(id, make, model, year, color, stock);
        }
    }

    internal class UpdateCarStockCommandHandler : ICommandHandler<UpdateCarStockCommand>
    {
        private readonly ICarStockRepository _repository;

        public UpdateCarStockCommandHandler(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateCarStockCommand command)
        {
            var carStock = _repository.Get(command.Car.Id);

            //may not necessary to throw exceptions
            if (carStock == null)
            {
                throw  new Exception("not found car for "+ command.Car.Id);
            }

            var updatedStock = carStock.Update(command.Car);

            _repository.Update(updatedStock);
        }
    }
}
