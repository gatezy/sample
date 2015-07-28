using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Car
{
    public class CreateCarStockViewModel
    {
        public Guid Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Stock { get; set; }
    }
}