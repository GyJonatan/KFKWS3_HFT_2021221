using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic.Results
{
    public class MostExpensiveBrandResult
    {
        public string BrandName { get; set; }
        public int NumberOfCars { get; set; }
        public int TotalPrice { get; set; }
        public override string ToString()
        {
            return $"Brand: {BrandName}\t Number of cars: {NumberOfCars} ({TotalPrice})";
        }
        public override bool Equals(object obj)
        {
            if (obj is MostExpensiveBrandResult)
            {
                MostExpensiveBrandResult other = obj as MostExpensiveBrandResult;
                return this.BrandName == other.BrandName
                    && this.NumberOfCars == other.NumberOfCars
                    && this.TotalPrice == other.TotalPrice;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return BrandName.GetHashCode() + TotalPrice + NumberOfCars;
        }
    }
}
