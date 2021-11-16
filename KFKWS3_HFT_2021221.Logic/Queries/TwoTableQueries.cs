using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class TwoTableQueries
    {
        ICarRepository carRepository;
        IBrandRepository brandRepository;
        ILeasingRepository leasingRepository;
        public TwoTableQueries
        (ICarRepository carRepository, IBrandRepository brandRepository, ILeasingRepository leasingRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
            this.leasingRepository = leasingRepository;
        }
        public IEnumerable<AveragesResult> GetBrandAverages()
        {
            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    let item = new { BrandName = brand.Name, Price = car.BasePrice }
                    group item by item.BrandName into grp
                    select new AveragesResult()
                    {
                        BrandName = grp.Key,
                        AveragePrice = grp.Average(item => item.Price) ?? 0
                    }).ToList();
        }
        //unit test
        public IEnumerable<MostCarsResult> GetBrandsByPopularity()
        {
            return (from brand in brandRepository.ReadAll()
                    join leasing in leasingRepository.ReadAll() on brand.LeasingId equals leasing.Id
                    let item = new { BrandName = brand.Name, NumberOfCars = brand.Cars.Count }
                    orderby item.NumberOfCars descending
                    select new MostCarsResult()
                    {
                        BrandName = item.BrandName,
                        LeasingName = leasing.Name,
                        NumberOfCars = item.NumberOfCars
                    }).ToList();
        }
        //unit test
        public IEnumerable<BudgetResult> GetDays()
        {
            return (brandRepository.ReadAll()
                     .Join(leasingRepository.ReadAll(),
                     brand => brand.LeasingId,
                     leasing => leasing.Id,
                     (brand, leasing) => new
                     {
                         leasingName = leasing.Name,
                         budget = leasing.Budget,
                         sumPrice = brand.Cars.Sum(x => x.BasePrice) ?? 0
                     })
                     .OrderBy(x => x.budget / x.sumPrice)
                     .Select(x => new BudgetResult()
                     {
                         leasingName = x.leasingName,
                         amountOfDays = x.budget / x.sumPrice
                     })).ToList();
        }

        //come up with something
        

    }
}
