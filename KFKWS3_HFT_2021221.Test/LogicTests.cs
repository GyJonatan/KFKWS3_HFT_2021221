using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using KFKWS3_HFT_2021221.Logic;
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
       [Test] 
       public void TestGetByBrand()
        {
            Mock<ICarRepository> mockedRepository = new Mock<ICarRepository>(MockBehavior.Loose);

            List<Car> cars = new List<Car>
            {
                new Car() { Model = "i440", BrandId = 1},
                new Car() { Model = "i440 Cabrio", BrandId = 1 },
                new Car() { Model = "Fiesta", BrandId = 2},
                new Car() { Model = "Arteon", BrandId = 3 }
            };

            List<Car> expectedCars = new List<Car>() { cars[0], cars[1] };
            
            mockedRepository.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            CarLogic logic = new CarLogic(mockedRepository.Object);

            var result = logic.GetCarsByBrand(1);

            Assert.That(result.Count, Is.EqualTo(expectedCars.Count));
            Assert.That(result, Is.EquivalentTo(expectedCars));

            mockedRepository.Verify(x => x.ReadAll(), Times.Once);
            mockedRepository.Verify(x => x.ReadOne(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void TestAddBrand()
        {
            Mock<IBrandRepository> brandRepository = new Mock<IBrandRepository>();
            brandRepository.Setup(x => x.Add(It.IsAny<string>())).Returns(42);
            BrandLogic logic = new BrandLogic(brandRepository.Object);

            int idNumber = logic.AddBrand("BMW");

            Assert.That(idNumber, Is.EqualTo(42));
            brandRepository.Verify(x => x.Add("BMW"), Times.Once);
        }
        
    }
}
