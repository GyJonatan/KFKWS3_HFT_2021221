using ConsoleTools;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KFKWS3_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);

            RestService rest = new RestService();
            MakeConnection(ref rest);

            var crudMenu = new ConsoleMenu();
            var mainMenu = new ConsoleMenu();
            var queryMenu = new ConsoleMenu();


            queryMenu
                .Add("AllDetails", () => GetQuery<Leasing>(rest,"AllDetails"))
                .Add("BrandAverages", () => GetQuery<AveragesResult>(rest, "BrandAverages"))
                .Add("CarsForLeasee", () => GetQuery<CarsWithExtraInfo>(rest, "CarsForLeasee"))
                .Add("CarsOverXPrice", () => GetQuery<CarsWithExtraInfo>(rest, "CarsOverXPrice"))
                .Add("CarsOrderedByBudget", () => GetQuery<CarsWithExtraInfo>(rest, "CarsOrderedByBudget"))
                .Add("LeaseeThatHasXBrand", () => GetQuery<Leasing>(rest, "LeaseeThatHasXBrand"))
                .Add("CarsLeasedInBudapest", () => GetQuery<CarsWithExtraInfo>(rest, "CarsLeasedInBudapest"))
                .Add("Back", () => mainMenu.Show())
                .Add("Close", ConsoleMenu.Close);


            crudMenu
                .Add("CRUD: cars", () => CarCruds(rest))
                .Add("CRUD: brands", () => BrandCruds(rest))
                .Add("CRUD: leasings", () => LeasingCruds(rest))
                .Add("Back", () => mainMenu.Show())
                .Add("Close", ConsoleMenu.Close);


            mainMenu
                .Add("CRUD Menu", () => crudMenu.Show())
                .Add("Query Menu", () => queryMenu.Show())
                .Add("Close", ConsoleMenu.Close);
            mainMenu.Show();

            

        }
        static void MakeConnection(ref RestService rest)
        {
            try
            {
                rest = new RestService("http://localhost:63437");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        static void GetAll<T>(RestService rest) where T : class
        {
            var items = rest.Get<T>(nameof(T).ToLower());
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
        
        static void GetQuery<T>(RestService rest, string query)
        {            
            var list = rest.Get<T>($"query/{query.ToLower()}");
            ;
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        
        static void CarCruds(RestService rest)
        {

            Console.WriteLine("Crud methods on a new Car:");
            
            Car car = new Car
            { BasePrice = 1, BrandId = 1, Model = "test" };

            rest.Post<Car>(car, "car");
            Console.WriteLine("New Car created and posted to the server.");
            

            int lastCarId = rest.Get<Car>("car").Count - 1;
            Car temp = rest.Get<Car>(lastCarId, "car");
            rest.Put<Car>(new Car()
            {
                BasePrice = temp.BasePrice,
                Brand = temp.Brand,
                BrandId = temp.BrandId,
                Id = temp.Id,
                Model = "put test"
            }, "car");
            Console.WriteLine("Car updated and put to the server.");

            rest.Delete(lastCarId, "car");
            Console.WriteLine("Latest car deleted.");

            var firstCar = rest.Get<Car>(1, "car");
            Console.WriteLine($"Car with Id 1 : {firstCar.Model}");

            var cars = rest.Get<Car>("car");
            foreach (Car item in cars)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void BrandCruds(RestService rest)
        {
            Console.WriteLine("Crud methods on a new Brand:");
            Brand brand = new Brand()
            { Name = "test", LeasingId = 1 };

            rest.Post<Brand>(brand, "brand");
            Console.WriteLine("new Brand created and posted to the server:");
            

            int lastBrandId = rest.Get<Brand>("brand").Count - 1;
            Brand temp = rest.Get<Brand>(lastBrandId, "brand");

            rest.Put<Brand>(new Brand()
            {
                Id = temp.Id, 
                Name = "put test", 
                Leasing = temp.Leasing, 
                LeasingId = temp.LeasingId
            }, "brand");
            Console.WriteLine("Brand updated and put to the server.");

            rest.Delete(lastBrandId, "brand");
            Console.WriteLine("Latest Brand deleted.");

            var firstBrand = rest.Get<Brand>(1, "brand");
            Console.WriteLine($"Brand with Id 1 : {firstBrand.Name}");

            var brands = rest.Get<Brand>("brand");
            foreach (Brand item in brands)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void LeasingCruds(RestService rest)
        {
            Console.WriteLine("Crud methods on a new Leasing:");
            Leasing leasing = new Leasing()
            { Budget = 1, HQLocation = "some location", Name = "test" };
            
            rest.Post<Leasing>(leasing, "leasing");
            Console.WriteLine("New Leasing created and posted to the server.");

            int lastleasingId = rest.Get<Leasing>("leasing").Count - 1;
            Leasing temp = rest.Get<Leasing>(lastleasingId, "leasing");
            rest.Put<Leasing>(new Leasing()
            {
                Budget = temp.Budget,
                HQLocation = temp.HQLocation,
                Id = temp.Id,
                Name = "put test"
            }, "leasing");
            Console.WriteLine("Leasing updated and put to the server.");

            rest.Delete(lastleasingId, "leasing");
            Console.WriteLine("Latest leasing deleted.");

            var firstLeasing = rest.Get<Leasing>(1, "leasing");
            Console.WriteLine($"Ceasing with Id 1 : {firstLeasing.Name}");

            var leasings = rest.Get<Leasing>("leasing");
            foreach (Leasing item in leasings)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
    }
}
