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
        List<CarsWithExtraInfo> expectedBrandByCarCount;
        List<Leasing> expectedAllDetails;
        List<CarsWithExtraInfo> expectedCarsForLeasee;
        List<CarsWithExtraInfo> expectedCarsOverXPrice;
        List<CarsWithExtraInfo> expectedCarsOrderedByBudget;
        List<Leasing> expectedLeaseeThatHasXBrand;
        List<CarsWithExtraInfo> expectedCarsLeasedInBudapest;

        private CarLogic TestGetAveragesMock()
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
        public void TestGetAverages()
        {
            var logic = TestGetAveragesMock();
            var actualAverages = logic.GetBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
        }
        private Query TestGetBrandAveragesMock()
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

            return new Query(carRepository.Object, brandRepository.Object);
        }

        [Test]
        public void TestGetBrandAverages()
        {
            var logic = TestGetBrandAveragesMock();
            var actualAverages = logic.GetBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(expectedAverages));
            carRepository.Verify(car => car.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }










        private Query TestGetAllDetailsMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1" };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2" };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };

                        
            Car car1 = new Car() { Brand = ford, BrandId = ford.Id };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedAllDetails = new List<Leasing>()
            {
                new Leasing() { Id = 1, Name = "leasing1" },
                new Leasing() { Id = 2, Name = "leasing2" }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetAllDetails()
        {
            var logic = TestGetAllDetailsMock();
            var actualQueryResult = logic.GetAllDetails();

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedAllDetails));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }








        private Query TestGetCarsForLeaseeMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1" };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2" };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };


            Car car1 = new Car() { Brand = ford, BrandId = ford.Id };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model1", BasePrice = 1 };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model2", BasePrice = 2 };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedCarsForLeasee = new List<CarsWithExtraInfo>()
            {
                new CarsWithExtraInfo() { BrandName = "BMW", Model = "model1", Price = 1 },
                new CarsWithExtraInfo() { BrandName = "BMW", Model = "model2", Price = 2 }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetCarsForLeasee()
        {
            var logic = TestGetCarsForLeaseeMock();
            var actualQueryResult = logic.GetCarsForLeasee("leasing1");

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedCarsForLeasee));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }








        private Query TestGetCarsOverXPriceMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1" };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2" };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };


            Car car1 = new Car() { Brand = ford, BrandId = ford.Id, Model = "model3", BasePrice = 200 };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model1", BasePrice = 100 };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model2", BasePrice = 50 };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedCarsOverXPrice = new List<CarsWithExtraInfo>()
            {
                new CarsWithExtraInfo() { LeasingName = "leasing2", BrandName = "Ford", Model = "model3", Price = 200 },
                new CarsWithExtraInfo() { LeasingName = "leasing1", BrandName = "BMW", Model = "model1", Price = 100 }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetCarsOverXPrice()
        {
            var logic = TestGetCarsOverXPriceMock();
            var actualQueryResult = logic.GetCarsOverXPrice(100);

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedCarsOverXPrice));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }








        private Query TestGetCarsOrderedByBudgetMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1", Budget = 1000 };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2", Budget = 2000 };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };


            Car car1 = new Car() { Brand = ford, BrandId = ford.Id, Model = "model3", BasePrice = 200 };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model1", BasePrice = 100 };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model2", BasePrice = 50 };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedCarsOrderedByBudget = new List<CarsWithExtraInfo>()
            {
                new CarsWithExtraInfo() { LeasingName = "leasing1", BrandName = "BMW", Model = "model1", Price = 100 },
                new CarsWithExtraInfo() { LeasingName = "leasing1", BrandName = "BMW", Model = "model2", Price = 50 },
                new CarsWithExtraInfo() { LeasingName = "leasing2", BrandName = "Ford", Model = "model3", Price = 200 }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetCarsOrderedByBudget()
        {
            var logic = TestGetCarsOrderedByBudgetMock();
            var actualQueryResult = logic.GetCarsOrderedByBudget(true);

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedCarsOrderedByBudget));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }








        private Query TestGetLeaseeThatHasXBrandMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1", Budget = 1000 };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2", Budget = 2000 };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };


            Car car1 = new Car() { Brand = ford, BrandId = ford.Id, Model = "model3", BasePrice = 200 };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model1", BasePrice = 100 };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model2", BasePrice = 50 };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedLeaseeThatHasXBrand = new List<Leasing>()
            {
                new Leasing() { Id = 1, Name = "leasing1", Budget = 1000 }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetLeaseeThatHasXBrand()
        {
            var logic = TestGetLeaseeThatHasXBrandMock();
            var actualQueryResult = logic.GetLeaseeThatHasXBrand("BMW");

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedLeaseeThatHasXBrand));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }








        private Query TestGetCarsLeasedInBudapestMock()
        {
            carRepository = new Mock<ICarRepository>();
            brandRepository = new Mock<IBrandRepository>();
            leasingRepository = new Mock<ILeasingRepository>();

            Leasing l1 = new Leasing() { Id = 1, Name = "leasing1", Budget = 1000, HQLocation = "Budapest" };
            Leasing l2 = new Leasing() { Id = 2, Name = "leasing2", Budget = 2000, HQLocation = "Debrecen" };



            Brand bmw = new Brand() { Id = 1, Name = "BMW", LeasingId = l1.Id };
            Brand ford = new Brand() { Id = 2, Name = "Ford", LeasingId = l2.Id };


            Car car1 = new Car() { Brand = ford, BrandId = ford.Id, Model = "model3", BasePrice = 200 };
            Car car2 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model1", BasePrice = 100 };
            Car car3 = new Car() { Brand = bmw, BrandId = bmw.Id, Model = "model2", BasePrice = 50 };



            List<Car> cars = new List<Car>() { car1, car2, car3 };
            List<Brand> brands = new List<Brand>() { bmw, ford };
            List<Leasing> leasings = new List<Leasing>() { l1, l2 };


            expectedCarsLeasedInBudapest = new List<CarsWithExtraInfo>()
            {
                new CarsWithExtraInfo() { LeasingName = "leasing1", BrandName = "BMW", Model = "model1", Price = 100 },
                new CarsWithExtraInfo() { LeasingName = "leasing1", BrandName = "BMW", Model = "model2", Price = 50 }
            };

            carRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());
            leasingRepository.Setup(x => x.ReadAll()).Returns(leasings.AsQueryable());

            return new Query(carRepository.Object, brandRepository.Object, leasingRepository.Object);
        }

        [Test]
        public void TestGetCarsLeasedInBudapest()
        {
            var logic = TestGetCarsLeasedInBudapestMock();
            var actualQueryResult = logic.GetCarsLeasedInBudapest();

            Assert.That(actualQueryResult, Is.EquivalentTo(expectedCarsLeasedInBudapest));

            carRepository.Verify(brand => brand.ReadAll(), Times.Once);
            brandRepository.Verify(brand => brand.ReadAll(), Times.Once);
            leasingRepository.Verify(brand => brand.ReadAll(), Times.Once);
        }
    }
}
