using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarStockService.Entity;

namespace WebApi.Models.Car
{
    public class CarStockViewModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Stock { get; set; }

        public CarStockViewModel()
        {
        }

        public CarStockViewModel(CarStock carStock)
        {
            this.Id = carStock.Id;
            this.Make = carStock.Make;
            this.Model = carStock.Model;
            this.Year = carStock.Year;
            this.Color = carStock.Color;
            this.Stock = carStock.Stock;
        }
    }
}