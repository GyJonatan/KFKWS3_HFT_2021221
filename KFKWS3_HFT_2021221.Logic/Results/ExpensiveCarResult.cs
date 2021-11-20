using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic.Results
{
    public class ExpensiveCarResult
    {
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string LeasingName { get; set; }
        public int Price { get; set; }
        public override string ToString()
        {
            return $"Leasing: {LeasingName}\t Brand: {BrandName}\tModel: {Model} ({Price})";
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpensiveCarResult)
            {
                ExpensiveCarResult other = obj as ExpensiveCarResult;
                return this.LeasingName == other.LeasingName
                    && this.BrandName == other.BrandName
                    && this.Model == other.Model
                    && this.Price == other.Price;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return LeasingName.GetHashCode()
                + BrandName.GetHashCode()
                + Model.GetHashCode()
                + Price;
        }
    }
}
