using System;
using System.Windows;
using KFKWS3_HFT_2021221.GUI;
using KFKWS3_HFT_2021221.Models;


namespace WpfClient
{
    public enum CrudType
    {
        Car,
        Brand,
        Leasing,
        None
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CrudType crudType = CrudType.None;
        public RestService rest;
        private void MakeConnection(ref RestService rest)
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

        private void GetQuery<T>(RestService rest, string query)
        {
            var list = rest.Get<T>($"query/{query.ToLower()}");
            
            foreach (var item in list)
            {
                displayText.Text += $"\n{item.ToString()}";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.Sleep(8000);

            rest = new RestService();
            MakeConnection(ref rest);

        }

        private void CarCrud_Click(object sender, RoutedEventArgs e)
        {
            CRUDOpened();
            crudType = CrudType.Car;
            displayText.Text = "";
            mainWindow.Title = "Crud methods on a new Car";
        }
        private void brandCrud_Click(object sender, RoutedEventArgs e)
        {
            CRUDOpened();
            crudType = CrudType.Brand;
            displayText.Text = "";
            mainWindow.Title = "Crud methods on a new Brand";
        }

        private void leasingCrud_Click(object sender, RoutedEventArgs e)
        {
            CRUDOpened();
            crudType = CrudType.Leasing;
            displayText.Text = $"{crudType}";
            mainWindow.Title = "Crud methods on a new Leasing";
        }

        private void queries_Click(object sender, RoutedEventArgs e)
        {            
            crudType = CrudType.None;
            displayText.Text = "";
            mainWindow.Title = "Queries";

            displayText.Text += "Leasees:";
            GetQuery<Leasing>(rest, "AllDetails");
            displayText.Text += "\n";
            displayText.Text += "\nBrand averages:";
            GetQuery<AveragesResult>(rest, "BrandAverages");

            displayText.Text += "\n";
            displayText.Text += "\nCars for lease:";
            GetQuery<CarsWithExtraInfo>(rest, "CarsForLeasee");

            displayText.Text += "\n";
            displayText.Text += "\nCars over X price:";
            GetQuery<CarsWithExtraInfo>(rest, "CarsOverXPrice");

            displayText.Text += "\n";
            displayText.Text += "\nCars ordered by budget:";
            GetQuery<CarsWithExtraInfo>(rest, "CarsOrderedByBudget");

            displayText.Text += "\n";
            displayText.Text += "\nLeasee that has X brand:";
            GetQuery<Leasing>(rest, "LeaseeThatHasXBrand");

            displayText.Text += "\n";
            displayText.Text += "\nCars leased in Budapest:";
            GetQuery<CarsWithExtraInfo>(rest, "CarsLeasedInBudapest");

        }

        private void CRUDOpened()
        {
            createButton.Visibility = Visibility.Visible;
            readButton.Visibility = Visibility.Visible;
            updateButton.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Visible;
            
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            switch (crudType)
            {
                case CrudType.Car:

                    Car car = new Car
                    { BasePrice = 1, BrandId = 1, Model = "teszt" };

                    rest.Post<Car>(car, "car");
                    displayText.Text += "\nNew Car created and posted to the server.";

                    break;
                case CrudType.Brand:

                    Brand brand = new Brand()
                    { Name = "teszt", LeasingId = 1 };

                    rest.Post<Brand>(brand, "brand");
                    displayText.Text += "\nNew Brand created and posted to the server:";

                    break;
                case CrudType.Leasing:

                    Leasing leasing = new Leasing()
                    { Budget = 1, HQLocation = "some location", Name = "teszt" };

                    rest.Post<Leasing>(leasing, "leasing");
                    displayText.Text += "\nNew Leasing created and posted to the server.";

                    break;
                case CrudType.None:



                    break;
                default:
                    break;
            }
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            switch (crudType)
            {
                case CrudType.Car:

                    var firstCar = rest.Get<Car>(1, "car");
                    displayText.Text += $"\nRead one:";
                    displayText.Text += $"\nCar with Id 1 : {firstCar.Model}";

                    displayText.Text += $"\nRead all:";
                    var cars = rest.Get<Car>("car");
                    foreach (Car item in cars)
                    {
                        displayText.Text += $"\n{item.ToString()}";
                    }

                    break;
                case CrudType.Brand:

                    var firstBrand = rest.Get<Brand>(1, "brand");
                    displayText.Text += $"\nBrand with Id 1 : {firstBrand.Name}";

                    var brands = rest.Get<Brand>("brand");
                    foreach (Brand item in brands)
                    {
                        displayText.Text += $"\n{item.ToString()}";
                    }

                    break;
                case CrudType.Leasing:

                    var firstLeasing = rest.Get<Leasing>(1, "leasing");
                    displayText.Text += $"\nCeasing with Id 1 : {firstLeasing.Name}";

                    var leasings = rest.Get<Leasing>("leasing");
                    foreach (Leasing item in leasings)
                    {
                        displayText.Text += $"\n{item.ToString()}";
                    }

                    break;
                case CrudType.None:
                    break;
                default:
                    break;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            switch (crudType)
            {
                case CrudType.Car:

                    int lastCarId = rest.Get<Car>("car").Count - 1;
                    Car carTemp = rest.Get<Car>(lastCarId, "car");
                    rest.Put<Car>(new Car()
                    {
                        BasePrice = carTemp.BasePrice,
                        Brand = carTemp.Brand,
                        BrandId = carTemp.BrandId,
                        Id = carTemp.Id,
                        Model = "put test"
                    }, "car");
                    displayText.Text += "\nCar updated and put to the server.";

                    break;
                case CrudType.Brand:

                    int lastBrandId = rest.Get<Brand>("brand").Count - 1;
                    Brand brandTemp = rest.Get<Brand>(lastBrandId, "brand");

                    rest.Put<Brand>(new Brand()
                    {
                        Id = brandTemp.Id,
                        Name = "put test",
                        Leasing = brandTemp.Leasing,
                        LeasingId = brandTemp.LeasingId
                    }, "brand");
                    displayText.Text += "\nBrand updated and put to the server.";

                    break;
                case CrudType.Leasing:

                    int lastleasingId = rest.Get<Leasing>("leasing").Count - 1;
                    Leasing temp = rest.Get<Leasing>(lastleasingId, "leasing");
                    rest.Put<Leasing>(new Leasing()
                    {
                        Budget = temp.Budget,
                        HQLocation = temp.HQLocation,
                        Id = temp.Id,
                        Name = "put test"
                    }, "leasing");
                    displayText.Text += "\nLeasing updated and put to the server.";

                    break;
                case CrudType.None:
                    break;
                default:
                    break;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (crudType)
            {
                case CrudType.Car:

                    int lastCarId = rest.Get<Car>("car").Count - 1;
                    rest.Delete(lastCarId, "car");
                    displayText.Text += "\nLatest Car deleted.";

                    break;
                case CrudType.Brand:

                    int lastBrandId = rest.Get<Brand>("brand").Count - 1;
                    rest.Delete(lastBrandId, "brand");
                    displayText.Text += "\nLatest Brand deleted.";

                    break;
                case CrudType.Leasing:

                    int lastleasingId = rest.Get<Leasing>("leasing").Count - 1;
                    rest.Delete(lastleasingId, "leasing");
                    displayText.Text += "\nLatest leasing deleted.";

                    break;
                case CrudType.None:
                    break;
                default:
                    break;
            }
        }        
    }
}
