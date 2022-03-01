using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public interface ILeasingRepository : IRepository<Leasing>
    {
        void Update(Leasing leasing);
        void ChangeCompanyName(int id, string newName);

    }
}
