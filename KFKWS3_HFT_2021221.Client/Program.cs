using ConsoleTools;
using KFKWS3_HFT_2021221.Models;
using System;

namespace KFKWS3_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //var menu = new ConsoleMenu()
            //    .Add("Using Logic", () => Dosomething(logic))
            //    .Add("Close", ConsoleMenu.Close);
            //menu.Show();

            System.Threading.Thread.Sleep(8000);

            RestService rest = new RestService("http://localhost:63437");

            rest.Post<Brand>(new Brand()
            {
                Name = "Peugeot"
            }, "brand");

            var cars = rest.Get<Car>("car");
            var brands = rest.Get<Brand>("brand");
            var leasings = rest.Get<Leasing>("leasing");


            //var result1 = rest.Get<Car>("/car");
        }
    }
}
