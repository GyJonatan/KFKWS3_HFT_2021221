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
        List<AveragesResult> expectedAverages;

        private CarAndBrandQuery CreateCarAndBrandQueryLogicWithMocks()
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
            return new CarAndBrandQuery(carRepository.Object, brandRepository.Object);
        }

        private CarLogic CreateCarLogicWithMocks()
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
        public void TestGetAveragesJoin()
        {
            var logic = CreateCarAndBrandQueryLogicWithMocks();
            var actualAverages = logic.GetBrandAveragesJoin();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }

        [Test]
        public void TestGetAverages()
        {
            var logic = CreateCarLogicWithMocks();
            var actualAverages = logic.GetBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
        }
    }
}
