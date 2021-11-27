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
    public class LeasingController : Controller<Leasing>
    {
        public LeasingController(ILeasingLogic logic) : base(logic) { }

        [HttpPut]
        public override void Put([FromBody] Leasing item)
        {
            (logic as ILeasingLogic).Update(item);
        }

        [HttpGet("changecompanyname/{id,name}")]
        public void ChangeName([FromRoute] int id, [FromRoute] string name)
        {
            (logic as ILeasingLogic).ChangeCompanyName(id, name);
        }
    }
}
