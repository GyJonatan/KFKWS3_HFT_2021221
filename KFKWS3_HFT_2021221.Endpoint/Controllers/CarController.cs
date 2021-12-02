using KFKWS3_HFT_2021221.Logic;
using KFKWS3_HFT_2021221.Logic.Interfaces;
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
    public class CarController : Controller<Car>
    {
        
        public CarController(ICarLogic logic) : base(logic) { }


        [HttpPut]
        public override void Put([FromBody] Car item)
        {
            (logic as ICarLogic).Update(item);
        }

        [HttpGet("changeprice/{id,price}")]
        public void ChangePrice([FromRoute] int id, [FromRoute] int price)
        {
            (logic as ICarLogic).ChangePrice(id, price);
        }

        [HttpGet("getbrandaverages")]
        public IList<AveragesResult> GetBrandAverages()
        {
            return (logic as ICarLogic).GetBrandAverages();
        }

    }
}
