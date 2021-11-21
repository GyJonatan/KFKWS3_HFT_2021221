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
    class ThreeTableQuery
    {
        ICarRepository carRepository;
        IBrandRepository brandRepository;
        ILeasingRepository leasingRepository;

        public ThreeTableQuery(ICarRepository carRepository, IBrandRepository brandRepository, ILeasingRepository leasingRepository)
        {
            this.carRepository = carRepository;
            this.brandRepository = brandRepository;
            this.leasingRepository = leasingRepository;
        }

        public IEnumerable<Leasing> GetALlDetails()
        {
            //lists all leasings inlcuding their brands and the cars of those
            return (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll() on brand.LeasingId equals leasing.Id
                    select leasing).ToList();
        }

        public IEnumerable<ExpensiveCarResult> GetAllLeaseesWhoPayForCarsThatCostMoreThan20K()
        {
            //returns the leasing name, brand name, model and price
            //of each car that costs more than 20k
           return  (from car in carRepository.ReadAll()
                    join brand in brandRepository.ReadAll() on car.BrandId equals brand.Id
                    join leasing in leasingRepository.ReadAll() on brand.LeasingId equals leasing.Id
                    where car.BasePrice >= 20000
                    select new ExpensiveCarResult()
                    {
                        BrandName = brand.Name,
                        LeasingName = leasing.Name,
                        Model = car.Model,
                        Price = car.BasePrice
                    }).ToList();
        }        
    }
}
