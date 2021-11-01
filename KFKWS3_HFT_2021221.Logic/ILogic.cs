using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    interface ILogic<T> where T : class
    {
        T ReadOne(int id);
        IList<T> ReadAll();
    }
}
