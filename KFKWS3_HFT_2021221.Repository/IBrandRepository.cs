using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    interface IBrandRepository
    {
        void Create(Brand car);
        Brand ReadOne(int id);
        IQueryable<Brand> ReadAll();
        void Update(Brand car);
        void Delete(int id);
    }
}
