using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarStockService.Command;
using CarStockService.Entity;
using CarStockService.Query;
using Common.CQRS;
using WebApi.Models.Car;

namespace WebApi.Controllers
{
    
    public class CarController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public CarController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }


        public IHttpActionResult Index()
        {
            return this.Ok();
        }

        [Route("CarStocks")]
        [HttpGet]
        public IList<CarStockViewModel> GetAll()
        {
            return
                _queryDispatcher.Request<AllCarStockQuery, IList<CarStock>>(new AllCarStockQuery())
                    .Select(x => new CarStockViewModel(x)).ToList();
        }

        [Route("CarStocks/{id}")]
        [HttpGet]
        public CarStockViewModel Get(Guid id)
        {
            return new CarStockViewModel(
                _queryDispatcher.Request<CarStockQuery, CarStock> (new CarStockQuery() {Id = id}));
        }

        [Route("CarStocks/create")]
        [HttpPost]
        public CarStockViewModel Create([FromBody] CreateCarStockViewModel createModel)
        {
            var newId = Guid.NewGuid();
            _commandDispatcher.Dispatch(new CreateCarStockCommand(newId,createModel.Make,createModel.Model,createModel.Year,createModel.Color,createModel.Stock));
            return new CarStockViewModel(
                _queryDispatcher.Request<CarStockQuery, CarStock>(new CarStockQuery() { Id = newId }));
        }

        [Route("CarStocks/update")]
        [HttpPost]
        public CarStockViewModel Update([FromBody] UpdateCarStockViewModel updateModel)
        {
            _commandDispatcher.Dispatch(new CreateCarStockCommand(updateModel.Id, updateModel.Make, updateModel.Model, updateModel.Year, updateModel.Color, updateModel.Stock));
            return new CarStockViewModel(
                _queryDispatcher.Request<CarStockQuery, CarStock>(new CarStockQuery() { Id = updateModel.Id }));
        }

        [Route("CarStocks/{id}/delete")]
        public IHttpActionResult Delete(Guid id)
        {
            _commandDispatcher.Dispatch(new DeleteCarStockCommand(){Id = id});
            return this.Ok();
        }
    }
}
