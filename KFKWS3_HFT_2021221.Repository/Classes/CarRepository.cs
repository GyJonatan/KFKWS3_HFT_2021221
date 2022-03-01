using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(KFKWS3DbContext context) : base(context) { }
        public override void Create(Car item)
        {
            context.Cars.Add(item);
            context.SaveChanges();
        }
        public override void Delete(int id)
        {
            Car car = ReadOne(id);

            context.Cars.Remove(car);
            context.SaveChanges();
        }
        public override Car ReadOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public void Update(Car car)
        {
            Car old = ReadOne(car.Id);

            if (old == null)
            {
                throw new InvalidOperationException($"***ERROR***\nUPDATE CAR: car({old.Id}) not found");
            }

            old.Model = car.Model;
            old.BasePrice = car.BasePrice;
            old.BrandId = car.BrandId;
        }
        public void ChangePrice(int id, int newPrice)
        {
            var car = ReadOne(id);
            if (car == null)
            {
                throw new InvalidOperationException($"***ERROR***\nCHANGE CAR PRICE: car({car.Id}) not found");
            }
            car.BasePrice = newPrice;
            context.SaveChanges();
        }
    }
}
