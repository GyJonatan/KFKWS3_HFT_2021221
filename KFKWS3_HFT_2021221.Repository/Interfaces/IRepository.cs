using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        T ReadOne(int id);
        IQueryable<T> ReadAll();
        void Delete(int id);
    }
}
