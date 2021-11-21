using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public class MostCarsResult
    {
        public string BrandName { get; set; }
        public string LeasingName { get; set; }
        public int NumberOfCars { get; set; }
        
        public override string ToString()
        {
            return $"Leasing: {LeasingName}\t Brand: {BrandName} ({NumberOfCars})";
        }
        public override bool Equals(object obj)
        {
            if (obj is MostCarsResult)
            {
                MostCarsResult other = obj as MostCarsResult;
                return this.BrandName == other.BrandName 
                    && this.LeasingName == other.LeasingName
                    && this.NumberOfCars == other.NumberOfCars;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return BrandName.GetHashCode() + LeasingName.GetHashCode() + NumberOfCars;
        }
    }
}
