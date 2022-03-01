using KFKWS3_HFT_2021221.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class Controller<T> : ControllerBase where T : class
    {
        protected ILogic<T> logic;

        public Controller(ILogic<T> logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void Post([FromBody] T item)
        {
            logic.Create(item);
        }

        [HttpGet]
        public IEnumerable<T> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public T Get(int id)
        {
            return logic.ReadOne(id);
        }

        [HttpPut("{item}")]
        public abstract void Put([FromBody] T item);

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            logic.Delete(id);
        }
    }
}
