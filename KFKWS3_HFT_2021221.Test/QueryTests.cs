using KFKWS3_HFT_2021221.Logic;
using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Test
{
    [TestFixture]
    public class QueryTests
    {
        Mock<ICarRepository> carRepository;
        Mock<IBrandRepository> brandRepository;
        Mock<ILeasingRepository> leasingRepository;


        List<AveragesResult> expectedAverages;
        List<MostCarsResult> expectedBrandByCarCount;
        private TwoTableQuery TestGetBrandAveragesFactory()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();

            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand ford = new Brand() { Id = 2, Name = "Ford" };

            List<Brand> brands = new List<Brand>() { bmw, ford };

            List<Car> cars = new List<Car>()
            {
                new Car() { Brand = bmw, BrandId = bmw.Id, BasePrice = 16000},
                new Car() { Brand = bmw, BrandId = bmw.Id, BasePrice = 15000},
                new Car() { Brand = ford, BrandId = ford.Id, BasePrice = 12000}
            };

            expectedAverages = new List<AveragesResult>()
            {
                new AveragesResult() {BrandName = "BMW", AveragePrice = 15500},
                new AveragesResult() {BrandName = "Ford", AveragePrice = 12000}
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            return new TwoTableQuery(carRepository.Object, brandRepository.Object);
        }

        private TwoTableQuery TestGetBrandsByCarCountFactory()
        {
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1" };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2" };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };



            Car carBmw1 = new Car() { Brand = bmw, BrandId = bmw.Id };
            Car carBmw2 = new Car() { Brand = bmw, BrandId = bmw.Id };
            Car carFord = new Car() { Brand = ford, BrandId = ford.Id };

            bmw.Cars.Add(carBmw1);
            bmw.Cars.Add(carBmw2);
            ford.Cars.Add(carFord);

            l1.Brands.Add(bmw);
            l2.Brands.Add(ford);

            List<Leasing> leasings = new List<Leasing>() { l1, l2 };
            List<Brand> brands = new List<Brand>() { bmw, ford };


            expectedBrandByCarCount = new List<MostCarsResult>()
            {
                new MostCarsResult() { BrandName = "BMW", LeasingName = "leasing1", NumberOfCars = 2 },
                new MostCarsResult() { BrandName = "Ford", LeasingName = "leasing2", NumberOfCars = 1 }
            };

            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            return new TwoTableQuery(brandRepository.Object, leasingRepository.Object);
        }

        private CarLogic TestGetAveragesFactory()
        {
            carRepository = new Mock<ICarRepository>();

            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand ford = new Brand() { Id = 2, Name = "Ford" };

            List<Car> cars = new List<Car>()
            {
                new Car() { Brand = bmw, BrandId = bmw.Id, BasePrice = 16000},
                new Car() { Brand = bmw, BrandId = bmw.Id, BasePrice = 15000},
                new Car() { Brand = ford, BrandId = ford.Id, BasePrice = 12000}
            };
            expectedAverages = new List<AveragesResult>()
            {
                new AveragesResult() {BrandName = "BMW", AveragePrice = 15500},
                new AveragesResult() {BrandName = "Ford", AveragePrice = 12000}
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            return new CarLogic(carRepository.Object);
        }

        [Test]
        public void TestGetBrandsByCarCount()
        {
            var logic = TestGetBrandsByCarCountFactory();
            var actualQueryResult = logic.GetBrandsByCarCount();

            Assert.That(actualQueryResult, Is.EqualTo(expectedBrandByCarCount));
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }

        [Test]
        public void TestGetBrandAverages()
        {
            var logic = TestGetBrandAveragesFactory();
            var actualAverages = logic.GetBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }

        [Test]
        public void TestGetAverages()
        {
            var logic = TestGetAveragesFactory();
            var actualAverages = logic.GetBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
        }


    }
}
