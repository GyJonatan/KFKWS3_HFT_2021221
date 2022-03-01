using KFKWS3_HFT_2021221.Models;
using KFKWS3_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            
            //Checks wether any properties are null
            //Throws exception if so
            foreach (var property in item.GetType()
                .GetProperties().Where(x => x.GetCustomAttribute<CanBeNull>() == null))
            {
                if (property.GetValue(item) is null)
                {
                    throw new NullReferenceException
                        ($"Creating was unsuccesfull:\t{property.Name} was null.");
                }
            }
            
            //Checks wether the same id is already on the list
            //Throws exception if so
            int id = int.Parse(item.GetType().GetProperty("Id").GetValue(item).ToString());

            if (ReadOne(id) is not null)
            {
                throw new InvalidOperationException
                    ($"Creating was unsuccesfull:\titem with id[{id}] already exists");
            }

            //By this point we are sure that the 'item' is good to go,
            //we can call the repo's create function.

            
            repository.Create(item);

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
