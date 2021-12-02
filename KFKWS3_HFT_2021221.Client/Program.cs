using ConsoleTools;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;

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
                .Add("Back", () => mainMenu.Show());


            crudMenu
                .Add("CRUD: cars", () => GetAll<Car>(rest))
                .Add("CRUD: brands", () => GetAll<Brand>(rest))
                .Add("CRUD: leasings", () => GetAll<Leasing>(rest))
                .Add("Back", () => mainMenu.Show())
                .Add("Close", ConsoleMenu.Close);


            mainMenu
                .Add("Exception test", () => 
                ExceptionTest<Brand>(rest, new Brand()
                {
                    Name = "Peugeot"
                }))
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
        static void ExceptionTest<T>(RestService rest, T item) where T : class
        {
            try
            {
                rest.Post<T>(item, nameof(T).ToLower());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }            
            catch (InvalidOperationException e)
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
            List<T> list = rest.GetQuery<T>(query, nameof(T).ToLower());

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
