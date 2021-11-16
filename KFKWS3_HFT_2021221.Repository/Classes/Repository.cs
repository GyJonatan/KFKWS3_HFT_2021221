using KFKWS3_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected KFKWS3DbContext context;
        public Repository(KFKWS3DbContext context)
        {
            this.context = context;
        }
        public abstract void Create(T item);
        public abstract void Delete(int id);       
        public IQueryable<T> ReadAll()
        {
            return context.Set<T>(); 
        }
        public abstract T ReadOne(int id);
    }
}
