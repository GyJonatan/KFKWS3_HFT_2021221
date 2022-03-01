using KFKWS3_HFT_2021221.Logic.Interfaces;
using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class Query : IQueryLogic
    {
        ICarRepository carRepository;
        IBrandRepository brandRepository;
        ILeasingRepository leasingRepository;

        public Query(ICarRepository carRepository, IBrandRepository brandRepository, ILeasingRepository leasingRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
            this.leasingRepository = leasingRepository;
        }
        public Query(ICarRepository carRepository, IBrandRepository brandRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
        }

        public Query(IBrandRepository brandRepository, ILeasingRepository leasingRepository)
        {
            this.brandRepository = brandRepository;
            this.leasingRepository = leasingRepository;
        }

        public IEnumerable<Leasing> GetAllDetails()
        {
            //lists all leasings inlcuding their brands and the cars of those
            var q = (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    select leasing).Distinct();
            return q;
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
                    });
        }

        public IEnumerable<CarsWithExtraInfo> GetCarsForLeasee(string leasingName)
        {
            //returns each car (with extra information)
            //for the leasee specified in the input

            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    where leasing.Name == leasingName
                    select new CarsWithExtraInfo
                    {
                        BrandName = brand.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    });
        }

        public IEnumerable<CarsWithExtraInfo> GetCarsOverXPrice(int price)
        {
            //returns a list of cars (with extra information)
            //which costs at least the amount specified by the input

            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    where car.BasePrice >= price
                    select new CarsWithExtraInfo
                    {
                        LeasingName = leasing.Name,
                        BrandName = brand.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    });
        }

        public IEnumerable<CarsWithExtraInfo> GetCarsOrderedByBudget(bool isAscending)
        {
            //returns the leasee's cars (with extra information) ordered by the
            //budget in chosen order

            return (isAscending ?
                    from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    orderby leasing.Budget
                    select new CarsWithExtraInfo
                    {
                        LeasingName = leasing.Name,
                        BrandName = brand.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    }
                    :
                    from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    orderby leasing.Budget descending
                    select new CarsWithExtraInfo
                    {
                        LeasingName = leasing.Name,
                        BrandName = brand.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    });                      
        }

        public IEnumerable<Leasing> GetLeaseeThatHasXBrand(string brandName)
        {
            //returns the leasee that has the brand that was specified by the input

            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    where brand.Name == brandName
                    select leasing).Distinct();
        }

        public IEnumerable<CarsWithExtraInfo> GetCarsLeasedInBudapest()
        {
            //returns the cars (with extra information) 
            //which belongs to a leasee who has it's HQ in Budapest

            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll()
                    on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll()
                    on brand.LeasingId equals leasing.Id
                    where leasing.HQLocation.Contains("Budapest")
                    select new CarsWithExtraInfo
                    {
                        LeasingName = leasing.Name,
                        BrandName = brand.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    });
        }
    }
}
