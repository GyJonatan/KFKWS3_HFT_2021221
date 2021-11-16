using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public interface ICarRepository : IRepository<Car>
    {        
        void Update(Car car);
        void ChangePrice(int id, int newPrice);

    }
}
