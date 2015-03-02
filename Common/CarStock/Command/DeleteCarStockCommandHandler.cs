
using System;
using CarStockService.Reposity;
using Common.CQRS;

namespace CarStockService.Command
{
    public class DeleteCarStockCommand : ICommand
    {
        public Guid Id { get; set; }
    }

    internal class DeleteCarStockCommandHandler : ICommandHandler<DeleteCarStockCommand>
    {
        private readonly ICarStockRepository _repository;

        public DeleteCarStockCommandHandler(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public void Handle(DeleteCarStockCommand command)
        {
            _repository.Delete(command.Id);
        }
    }
}
