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

        public void Update(Leasing leasing)
        {
            (repository as ILeasingRepository).Update(leasing);
        }



        //useful for later reference

        //public static void AddToCompanyList<T>(this Leasing leasing, Company company)
        //{
        //    if (!leasing.Companies.Contains(company))
        //    {
        //        leasing.Companies.Add(company);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Company is already on the list");
        //    }
        //}
    }
}
