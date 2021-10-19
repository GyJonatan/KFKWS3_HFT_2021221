using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    class BrandRepository : IBrandRepository
    {
        KFKWS3DbContext context;

        public BrandRepository(KFKWS3DbContext context)
        {
            this.context = context;
        }

        public void Create(Brand brand)
        {
            context.Brands.Add(brand);
            context.SaveChanges();
        }

        public void Delete(int id)
        {         
            Brand brand = ReadOne(id);

            context.Brands.Remove(brand);
            context.SaveChanges();
        }

        public Brand ReadOne(int id)
        {
            return context
                .Brands
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return context.Brands;
        }

        public void Update(Brand brand)
        {
            Brand old = ReadOne(brand.Id);

            if (old == null)
            {
                throw new NullReferenceException();
            }

            old.Id = brand.Id;
            old.Name = brand.Name;
        }
    }
}
