using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    interface ILeasingRepository
    {
        void Create(Leasing car);
        Leasing ReadOne(int id);
        IQueryable<Leasing> ReadAll();
        void Update(Leasing car);
        void Delete(int id);
    }
}
