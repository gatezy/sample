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
        public Guid Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public int Stock { get; set; }

        public CreateCarStockCommand(Guid id, string make, string model, int year, string color, int stock)
        {
            this.Id = id;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Color = color;
            this.Stock = stock;
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
            _repository.Create(new CarStock(command.Id, command.Make, command.Model, command.Year, command.Color,
                command.Stock));
        }
    }
}
