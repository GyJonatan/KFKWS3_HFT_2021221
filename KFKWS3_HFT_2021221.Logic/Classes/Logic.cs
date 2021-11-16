using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Logic
{
    public abstract class Logic<T> : ILogic<T> where T : class
    {
        protected IRepository<T> repository;
        public Logic(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public IList<T> ReadAll()
        {
            return repository.ReadAll().ToList();
        }
        public T ReadOne(int id)
        {
            return repository.ReadOne(id);
        }
    }
}
