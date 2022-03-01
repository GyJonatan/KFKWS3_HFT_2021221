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
        public void Update(Brand brand)
        {
            (repository as IBrandRepository).Update(brand);
        }
        public void ChangeName(int carId, string newName)
        {
            (repository as IBrandRepository).ChangeName(carId, newName);
        }
        IList<Brand> GetAll()
        {
            return (repository as IBrandRepository).ReadAll().ToList();
        }
        public int Add(string name)
        {
            return (repository as IBrandRepository).Add(name);
        }
        
    }
}
