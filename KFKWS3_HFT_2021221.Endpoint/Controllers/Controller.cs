using KFKWS3_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Endpoint.Controllers
{
    public abstract class Controller<T> : ControllerBase where T : class
    {
        protected ILogic<T> logic;

        public Controller(ILogic<T> logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<T> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] T item)
        {
            logic.Create(item);
        }

        [HttpPut]
        public abstract void Edit([FromBody] T item);

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            logic.Delete(id);
        }
    }
}
