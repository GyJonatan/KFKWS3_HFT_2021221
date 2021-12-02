using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Models
{
    public class CarsWithExtraInfo
    {
        public string BrandName { get; set; }
        public string LeasingName { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Leasing: {LeasingName}\tBrand: {BrandName}\tModel: {Model} ({Price})";
        }
        public override bool Equals(object obj)
        {
            if (obj is CarsWithExtraInfo)
            {
                CarsWithExtraInfo other = obj as CarsWithExtraInfo;
                return this.BrandName == other.BrandName
                    && this.LeasingName == other.LeasingName
                    && this.Price == other.Price
                    && this.Model == other.Model;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return BrandName.GetHashCode()
                 + LeasingName.GetHashCode()
                 + Model.GetHashCode()
                 + Price;
        }
    }
}
