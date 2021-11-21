using KFKWS3_HFT_2021221.Logic.Results;
using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class TwoTableQuery
    {
        ICarRepository carRepository;
        IBrandRepository brandRepository;
        ILeasingRepository leasingRepository;
        public TwoTableQuery(ICarRepository carRepository, IBrandRepository brandRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
        }

        public TwoTableQuery(IBrandRepository brandRepository, ILeasingRepository leasingRepository)
        {
            this.brandRepository = brandRepository;
            this.leasingRepository = leasingRepository;
        }
        public IEnumerable<AveragesResult> GetBrandAverages()
        {
            //gets the average price of each brand
            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    let item = new { BrandName = brand.Name, Price = car.BasePrice }
                    group item by item.BrandName into grp
                    select new AveragesResult()
                    {
                        BrandName = grp.Key,
                        AveragePrice = grp.Average(item => item.Price)
                    }).ToList();
        }
        //unit test
        public IEnumerable<MostCarsResult> GetBrandsByCarCount()
        {            
            return (from brand in brandRepository.ReadAll()
                    join leasing in leasingRepository.ReadAll() on brand.LeasingId equals leasing.Id
                    select new MostCarsResult()
                    {
                        BrandName = brand.Name,
                        LeasingName = leasing.Name,
                        NumberOfCars = brand.Cars.Count
                    }).OrderByDescending(x => x.NumberOfCars).ToList();
        }
        //unit test
        public IEnumerable<BudgetResult> GetDays()
        {
            //gets how many days could each leasee pay for
            //if they were to rent every car they can
            return (brandRepository.ReadAll()
                     .Join(leasingRepository.ReadAll(),
                     brand => brand.LeasingId,
                     leasing => leasing.Id,
                     (brand, leasing) => new
                     {
                         leasingName = leasing.Name,
                         budget = leasing.Budget,
                         sumPrice = brand.Cars.Sum(x => x.BasePrice)
                     })
                     .OrderBy(x => x.budget / x.sumPrice)
                     .Select(x => new BudgetResult()
                     {
                         leasingName = x.leasingName,
                         amountOfDays = x.budget / x.sumPrice
                     })).ToList();
        }


        public IEnumerable<MostExpensiveBrandResult> GetMostExpensiveBrand()
        {
            //return every brand name, number of its cars and the sum of the prices of these cars
            //where the car costs more than 20k
            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    let item = new { brand.Name, car.BasePrice, brand.Cars.Count }
                    group item by item.Name into g
                    select new MostExpensiveBrandResult()
                    {
                        BrandName = g.Key,
                        NumberOfCars = g.Count(x => x.BasePrice >= 20000),
                        TotalPrice = g.Where(x => x.BasePrice >= 20000).Sum(x => x.BasePrice)
                    }).ToList();
        }
        
        

    }
}
