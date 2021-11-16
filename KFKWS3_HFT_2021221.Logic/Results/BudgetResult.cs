using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class BudgetResult
    {
        //This is something i have to come up with later on.
        //It'll show how much each company pays for their cars.


        public string leasingName { get; set; }
        public int amountOfDays { get; set; }
        public override string ToString()
        {
            return $"Leasing: {leasingName}\t Days: {amountOfDays}";
        }
        public override bool Equals(object obj)
        {
            if (obj is BudgetResult)
            {
                BudgetResult other = obj as BudgetResult;
                return this.leasingName == other.leasingName
                    && this.amountOfDays == other.amountOfDays;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return leasingName.GetHashCode() + amountOfDays;
        }
    }
}
