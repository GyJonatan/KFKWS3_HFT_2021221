using KFKWS3_HFT_2021221.Logic;
using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KFKWS3_HFT_2021221.Test
{
    //NuGet: Nunit,Nunit3TestAdapter, Moq, Microsoft.NET.Test.Sdk
    //5x Non-crud
    //3x Create
    //2x Something

    [TestFixture]
    public class LogicTests
    {

        Mock<ICarRepository> mockedCarRepository;

       [Test] 
       public void TestGetByBrand()
        {
            mockedCarRepository = new Mock<ICarRepository>(MockBehavior.Loose);

            List<Car> cars = new List<Car>
            {
                new Car() { Model = "i440", BrandId = 1},
                new Car() { Model = "i440 Cabrio", BrandId = 1 },
                new Car() { Model = "Fiesta", BrandId = 2},
                new Car() { Model = "Arteon", BrandId = 3 }
            };

            List<Car> expectedCars = new List<Car>() { cars[0], cars[1] };

            mockedCarRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            CarLogic logic = new CarLogic(mockedCarRepository.Object);

            var result = logic.GetCarsByBrand(1);

            Assert.That(result.Count, Is.EqualTo(expectedCars.Count));
            Assert.That(result, Is.EquivalentTo(expectedCars));

            mockedCarRepository.Verify(x => x.ReadAll(), Times.Once);
            mockedCarRepository.Verify(x => x.ReadOne(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void TestAddBrand()
        {
            Mock<IBrandRepository> brandRepository = new Mock<IBrandRepository>();
            brandRepository.Setup(x => x.Add(It.IsAny<string>())).Returns(42);
            BrandLogic logic = new BrandLogic(brandRepository.Object);

            int idNumber = logic.Add("BMW");

            Assert.That(idNumber, Is.EqualTo(42));
            brandRepository.Verify(x => x.Add("BMW"), Times.Once);
        }

        [Test]
        public void LogicCreateWithNullName()
        {
            mockedCarRepository = new Mock<ICarRepository>(MockBehavior.Loose);
            CarLogic logic = new CarLogic(mockedCarRepository.Object);

            Car NullObject = new Car();
            Assert.Throws(typeof(NullReferenceException),
                () => logic.Create(NullObject));
        }

        [Test]
        public void LogicCreateWithAlreadyExistingId()
        {
            mockedCarRepository = new Mock<ICarRepository>(MockBehavior.Loose);
            Car car1 = new Car()
            { 
                Id = 1,
                BasePrice = 1,
                Brand = new Brand(),
                BrandId = 1,
                Model = "model"
            };

            mockedCarRepository.Setup(x => x.Create(car1));

            CarLogic logic = new CarLogic(mockedCarRepository.Object);

            Assert.That(() => logic.Create(car1), Throws.Nothing);

        }

        [Test]
        public void LogicDeleteWithWrongId()
        {
            mockedCarRepository = new Mock<ICarRepository>(MockBehavior.Loose);

            List<Car> cars = new List<Car>
            {
                new Car() { Id = 1 },
                new Car() { Id = 2 }
            };

            mockedCarRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            CarLogic logic = new CarLogic(mockedCarRepository.Object);

            Assert.Throws(typeof(NullReferenceException), 
                () => logic.Delete(12));            
        }
        
    }
}
