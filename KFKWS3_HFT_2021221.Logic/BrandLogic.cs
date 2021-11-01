using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class BrandLogic : Logic<Brand>, IBrandLogic
    {
        public BrandLogic(IBrandRepository repository) : base(repository) { }
        public void ChangeName(int carId, string newName)
        {
            (repository as IBrandRepository).ChangeName(carId, newName);
        }
        public IList<MostCarsResult> Dosomething()
        {
            //this is something i have to come up with later on
            //It'll list the companies ordered by the amount of cars they are leasing
            //in descending order.
            return null; //for the meantime the result is null.
        }
        IList<Brand> GetAllBrands()
        {
            return repository.ReadAll().ToList();
        }
        public int AddBrand(string brandName)
        {
            return (repository as IBrandRepository).Add(brandName);
        }
    }
}
