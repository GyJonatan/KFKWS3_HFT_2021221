using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    class CarRepository : ICarRepository
    {
        KFKWS3DbContext context;

        public CarRepository(KFKWS3DbContext context)
        {
            this.context = context;
        }

        public void Create(Car car)
        {
            context.Cars.Add(car);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Car car = ReadOne(id);

            context.Cars.Remove(car);
            context.SaveChanges();
        }

        public Car ReadOne(int id)
        {
            return context
                .Cars
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Car> ReadAll()
        {
            return context.Cars;
        }

        public void Update(Car car)
        {
            Car old = ReadOne(car.Id);
            if (old == null)
            {
                throw new NullReferenceException();
            }
            old.Model = car.Model;
            old.BasePrice = car.BasePrice;
            old.BrandId = car.BrandId;
        }

    }
}
