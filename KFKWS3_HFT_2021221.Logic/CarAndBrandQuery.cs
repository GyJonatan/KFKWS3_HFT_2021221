using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class CarAndBrandQuery
    {
        ICarRepository carRepository;
        IBrandRepository brandRepository;
        public CarAndBrandQuery(ICarRepository carRepository, IBrandRepository brandRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
        }
        public IList<AveragesResult> GetBrandAveragesJoin()
        {
            var q = from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    let item = new {BrandName = brand.Name, Price = car.BasePrice }
                    group item by item.BrandName into grp
                    select new AveragesResult()
                    {
                        BrandName = grp.Key,
                        AveragePrice = grp.Average(item => item.Price) ?? 0
                    };
            return q.ToList();
        }
    }
}
