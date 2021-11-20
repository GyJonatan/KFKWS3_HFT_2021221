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
        public void Create(T item)
        {
            foreach (var property in item.GetType().GetProperties())
            {
                if (property.GetValue(item) == null)
                {
                    throw new NullReferenceException($"Creating was unsuccesfull:\t{property.Name} was null.");
                }
                
                repository.Create(item);
            }
        }
        public IList<T> ReadAll()
        {
            return repository.ReadAll().ToList();
        }
        public T ReadOne(int id)
        {
            return repository.ReadOne(id);
        }
        public void Delete(int id)
        {
            if (ReadOne(id) == null)
            {
                throw new NullReferenceException($"Deleting was unsuccesfull:\t{id} couldn't be found.");
            }
            
            repository.Delete(id);
        }
    }
}
