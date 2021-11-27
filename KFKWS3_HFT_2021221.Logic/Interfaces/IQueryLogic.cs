using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic.Interfaces
{
    public interface IQueryLogic
    {
        public IEnumerable<Leasing> GetAllDetails();
        public IEnumerable<AveragesResult> GetBrandAverages();
        public IEnumerable<CarsWithExtraInfo> GetCarsForLeasee(string name);
        public IEnumerable<CarsWithExtraInfo> GetCarsOverXPrice(int price);
        public IEnumerable<CarsWithExtraInfo> GetCarsOrderedByBudget(bool isAscending);
        public IEnumerable<Leasing> GetLeaseeThatHasXBrand(string brand);
        public IEnumerable<CarsWithExtraInfo> GetCarsLeasedInBudapest();
    }
}
