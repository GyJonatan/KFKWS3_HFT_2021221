using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public interface ICarLogic : ILogic<Car>
    {
        void ChangePrice(int carId, int price);
        void Update(Car car);
        IList<AveragesResult> GetBrandAverages();
    }
}
