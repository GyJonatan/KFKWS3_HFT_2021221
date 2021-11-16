using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        int Add(string name);
        void Update(Brand brand);
        void ChangeName(int id, string newName);
    }
}
