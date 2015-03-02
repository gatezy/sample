using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Command
{
    public class UpdateCarStockCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Stock { get; set; }

        public UpdateCarStockCommand(Guid id,string make, string model, int year, string color, int stock)
        {
            this.Id = id;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Color = color;
            this.Stock = stock;
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
            var carStock = _repository.Get(command.Id);
            var updatedStock = carStock.Update(command.Make, command.Model, command.Year, command.Color,
                command.Stock);

            _repository.Update(updatedStock);
        }
    }
}
