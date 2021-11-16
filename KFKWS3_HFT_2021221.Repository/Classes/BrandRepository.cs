using KFKWS3_HFT_2021221.Data;
using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(KFKWS3DbContext context) : base(context) { }     
        public override void Create(Brand item)
        {
            context.Brands.Add(item);
            context.SaveChanges();
        }

        public override void Delete(int id)
        {
            Brand brand = ReadOne(id);

            context.Brands.Remove(brand);
            context.SaveChanges();
        }

        public override Brand ReadOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }

        public void Update(Brand brand)
        {
            Brand old = ReadOne(brand.Id);

            if (old == null)
            {
                throw new InvalidOperationException($"***ERROR***\nUPDATE BRAND: brand({old.Id}) not found");
            }

            old.Id = brand.Id;
            old.Name = brand.Name;
        }
        public void ChangeName(int id, string newName)
        {
            var brand = ReadOne(id);
            if (brand == null)
            {
                throw new InvalidOperationException($"***ERROR***\nCHANGE BRAND NAME: brand({brand.Id}) not found");
            }
            brand.Name = newName;
            context.SaveChanges();
        }

        public int Add(string name)
        {
            Brand brand = new Brand();
            brand.Name = name;
            context.Set<Brand>().Add(brand);
            context.SaveChanges();
            return brand.Id;
        }
    }    
}
