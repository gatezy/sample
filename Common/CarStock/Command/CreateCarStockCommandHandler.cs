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
    public class CreateCarStockCommand : ICommand
    {
        public CarStock Car { get; private set; }

        public CreateCarStockCommand(Guid id, string make, string model, int year, string color, int stock)
        {
            this.Car = new CarStock(id, make, model, year, color, stock);
        }
    }

    internal class CreateCarStockCommandHandler : ICommandHandler<CreateCarStockCommand>
    {
        private readonly ICarStockRepository _repository;

        public CreateCarStockCommandHandler(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateCarStockCommand command)
        {
            _repository.Create(command.Car);
        }
    }
}
