using System;
using System.Linq;

namespace KFKWS3_HFT_2021221.Logic
{
    public class AveragesResult
    {
        public string BrandName { get; set; }
        public double AveragePrice { get; set; }
        public override string ToString()
        {
            return $"Brand: {BrandName}\nAverage Price: {AveragePrice}";
        }
        public override bool Equals(object obj)
        {
            if (obj is AveragesResult)
            {
                AveragesResult other = obj as AveragesResult;
                return this.BrandName == other.BrandName && this.AveragePrice == other.AveragePrice;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return BrandName.GetHashCode() + (int)AveragePrice;
        }

    }
}
