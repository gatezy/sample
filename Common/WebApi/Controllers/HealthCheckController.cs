using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("v1/healthcheck")]
    public class HealthCheckController : ApiController
    {
        /// <summary>
        /// health check for aws
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Index()
        {
            return this.Ok();
        }
    }
}
