using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CarStockService.Command;
using CarStockService.Entity;
using CarStockService.Query;
using Common.CQRS;
using WebApi.Models.Car;

namespace WebApi.Controllers
{
    [RoutePrefix("v1/CarStocks")]
    public class CarController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public CarController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IList<CarStockViewModel>))]
        public IHttpActionResult GetAll()
        {
            var result = _queryDispatcher.Request<AllCarStockQuery, IList<CarStock>>(new AllCarStockQuery()).Select(x => new CarStockViewModel(x)).ToList();
            return this.Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(CarStockViewModel))]
        public IHttpActionResult Get(Guid id)
        {
            return this.Ok(new CarStockViewModel(_queryDispatcher.Request<CarStockQuery, CarStock> (new CarStockQuery() {Id = id})));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateCarStockViewModel createModel)
        {
            _commandDispatcher.Dispatch(new CreateCarStockCommand(createModel.Id, createModel.Make, createModel.Model, createModel.Year, createModel.Color, createModel.Stock));
            return this.Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateCarStockViewModel updateModel)
        {
            _commandDispatcher.Dispatch(new CreateCarStockCommand(updateModel.Id, updateModel.Make, updateModel.Model, updateModel.Year, updateModel.Color, updateModel.Stock));
            return this.Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _commandDispatcher.Dispatch(new DeleteCarStockCommand(){Id = id});
            return this.Ok();
        }
    }
}
