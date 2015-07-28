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
    [RoutePrefix("v1/carstocks")]
    public class CarController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public CarController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// get all the car stocks
        /// </summary>
        /// <response code="500">internal server error</response>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IList<CarStockViewModel>))]
        public IHttpActionResult GetAll()
        {
            var result = _queryDispatcher.Request<AllCarStockQuery, IList<CarStock>>(new AllCarStockQuery()).Select(x => new CarStockViewModel(x)).ToList();
            return this.Ok(result);
        }

        /// <summary>
        /// get car stock by id
        /// </summary>
        /// <response code="404">record not found</response>
        /// <response code="500">internal server error</response>
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(CarStockViewModel))]
        public IHttpActionResult Get(Guid id)
        {
            var car = _queryDispatcher.Request<CarStockQuery, CarStock>(new CarStockQuery() {Id = id});

            if (car == null)
            {
                return this.NotFound();
            }

            return this.Ok(new CarStockViewModel(car));
        }

        /// <summary>
        /// create a new car stock record
        /// </summary>
        /// <response code="500">internal server error</response>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateCarStockViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            _commandDispatcher.Dispatch(new CreateCarStockCommand(createModel.Id, createModel.Make, createModel.Model, createModel.Year, createModel.Color, createModel.Stock));
            return this.Ok();
        }

        /// <summary>
        /// update a new car stock record
        /// </summary>
        /// <response code="500">internal server error</response>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateCarStockViewModel updateModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            _commandDispatcher.Dispatch(new UpdateCarStockCommand(updateModel.Id, updateModel.Make, updateModel.Model, updateModel.Year, updateModel.Color, updateModel.Stock));
            return this.Ok();
        }

        /// <summary>
        /// delete a new car stock record
        /// </summary>
        /// <response code="500">internal server error</response>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _commandDispatcher.Dispatch(new DeleteCarStockCommand(){Id = id});
            return this.Ok();
        }
    }
}
