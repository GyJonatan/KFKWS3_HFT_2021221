using KFKWS3_HFT_2021221.Logic;
using KFKWS3_HFT_2021221.Models;
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
    public class BrandController : Controller<Brand>
    {
        public BrandController(IBrandLogic logic) : base(logic) { }

        [HttpPut]
        public override void Put([FromBody] Brand item)
        {
            (logic as IBrandLogic).Update(item);
        }
        [HttpGet("changename/{id,name}")]
        public void ChangeName([FromRoute] int id, [FromRoute] string name)
        {
            (logic as IBrandLogic).ChangeName(id, name);
        }

        [HttpGet("add/{name}")]
        public void Add([FromRoute] string name)
        {
            (logic as IBrandLogic).Add(name);
        }
    }
}
