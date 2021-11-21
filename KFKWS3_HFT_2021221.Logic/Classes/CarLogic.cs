using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class CarLogic : Logic<Car>, ICarLogic 
    {
        public CarLogic(ICarRepository repository) : base(repository) { }        
        public void Update(Car car)
        {
            (repository as ICarRepository).Update(car);
        }
        public void ChangePrice(int carId, int price)
        {
            (repository as ICarRepository).ChangePrice(carId, price);
        }
        public IList<AveragesResult> GetBrandAverages()
        {
            var q = from car in repository.ReadAll()
                    group car by new
                    { 
                        car.Brand.Id, 
                        car.Brand.Name
                    }
                    into grp
                    select new AveragesResult
                    {
                        BrandName = grp.Key.Name,
                        AveragePrice = grp.Average(x => x.BasePrice)
                    };

            return q.ToList();
        }
        public List<Car> GetCarsByBrand(int brand)
        {
            return repository.ReadAll().Where(x => x.BrandId == brand).ToList();
        }
        
    }
}
