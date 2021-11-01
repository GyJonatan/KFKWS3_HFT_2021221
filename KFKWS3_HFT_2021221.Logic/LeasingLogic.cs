using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class LeasingLogic : Logic<Leasing>, ILeasingLogic
    {
        public LeasingLogic(ILeasingRepository repository) : base(repository) { }        
        public void ChangeCompanyName(int id, string newName)
        {
            (repository as ILeasingRepository).ChangeCompanyName(id, newName);
        }

        public string MostPayingCompany()
        {
            //This is something i have to come up with later on.
            //It'll show the name of the company that pays the most for it's leased cars.
            return null; //for the meantime the result is null.
        }
    }
}
