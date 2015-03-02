using System;

namespace CarStockService.Entity
{
    //car stock will be transferred between web tier and service tier
    //but only service tier can trigger the biz logic methods
    //partial class may be a better implementation
    public class CarStock
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public string Color { get; set; }

        public int Stock { get; set; }

        internal CarStock()
        {
        }

        //this is for create new stock
        internal CarStock(Guid id, string make, string model, int year, string color, int stock)
        {
            this.Id = id;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Color = color;
            this.Stock = stock;
        }

        //this looks like a duplicate of previous constructor
        //but this is a domain object, inner biz logic should be implemented here. 
        //like partial update also triggers the status update, etc...
        internal CarStock Update(string make, string model, int year, string color, int stock)
        {
            return new CarStock(this.Id,
                make,
                model,
                year,
                color,
                stock);
        }
    }
}
