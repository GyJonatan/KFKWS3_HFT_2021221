using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{
    public class Company
    {
        public string Name { get; set; }
        public int TaxNumber { get; set; }
        public string LocationOfHQ { get; set; }
        public DateTime FoundingDate { get; set; }
        public int Budget { get; set; }
    }
}
