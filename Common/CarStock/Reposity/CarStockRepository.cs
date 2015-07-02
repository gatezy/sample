using System;
using System.Collections.Generic;
using System.Linq;
using CarStockService.Entity;

namespace CarStockService.Reposity
{
    public interface ICarStockRepository
    {
        CarStock Get(Guid id);
        IList<CarStock> All();
        void Create(CarStock carStock);
        void Update(CarStock carStock);
        void Delete(Guid id);
    }

    internal class CarStockRepository : ICarStockRepository
    {
        private static IList<CarStock> _carStocks;

        static CarStockRepository()
        {
            _carStocks = new List<CarStock>()
            {
                    new CarStock(Guid.NewGuid(), "Honda","Civic",2014,"Red",10),
                    new CarStock(Guid.NewGuid(), "Honda","Civic",2014,"Blue",20),
                    new CarStock(Guid.NewGuid(), "Honda","Civic",2014,"Black",30),
                    new CarStock(Guid.NewGuid(), "Toyota","Corolla",2015,"Red",10),
                    new CarStock(Guid.NewGuid(), "Toyota","Corolla",2015,"Blue",20),
                    new CarStock(Guid.NewGuid(), "Toyota","Corolla",2015,"Black",30),
            };
        }

        public CarStock Get(Guid id)
        {
            return _carStocks.FirstOrDefault(x => x.Id == id);
        }

        public IList<CarStock> All()
        {
            return _carStocks;
        }

        public void Create(CarStock carStock)
        {
            if (_carStocks.Any(x=>x.Id == carStock.Id))
            {
                throw new Exception("duplicate id " + carStock.Id);
            }

            _carStocks.Add(carStock);
        }

        public void Update(CarStock carStock)
        {
            if (!_carStocks.Any(x => x.Id == carStock.Id))
            {
                throw new Exception("no stock for " + carStock.Id);
            }

            //a bit lazy, for convenient update
            _carStocks.Remove(_carStocks.First(x => x.Id == carStock.Id));
            _carStocks.Add(carStock);
        }

        public void Delete(Guid id)
        {
            if (!_carStocks.Any(x => x.Id == id))
            {
                throw new Exception("no stock for " + id);
            }

            _carStocks.Remove(_carStocks.First(x => x.Id == id));
        }
    }
}
