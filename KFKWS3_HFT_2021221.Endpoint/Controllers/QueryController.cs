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
    [Route("[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        IQueryLogic logic;
        public QueryController(IQueryLogic logic)
        {
            this.logic = logic;
        }

        //[HttpGet("alldetails")]
        public IList<Leasing> AllDetails()
        {
            return logic.GetAllDetails().ToList();
        }

        [HttpGet("brandaverages")]
        public IList<AveragesResult> BrandAverages()
        {
            return logic.GetBrandAverages().ToList();
        }

        [HttpGet("carsforleasee/{name}")]
        public IList<CarsWithExtraInfo> CarsForLeasee([FromRoute] string name)
        {
            return logic.GetCarsForLeasee(name).ToList();
        }

        [HttpGet("carsoverxprice/{price}")]
        public IList<CarsWithExtraInfo> CarsOverXPrice([FromRoute] int price)
        {
            return logic.GetCarsOverXPrice(price).ToList();
        }

        [HttpGet("carsorderedbybudget/{order}")]
        public IList<CarsWithExtraInfo> CarsOrderedByBudget([FromRoute] string order)
        {
            return order == "asc" ?
                logic.GetCarsOrderedByBudget(true).ToList() :
                logic.GetCarsOrderedByBudget(false).ToList();
        }

        [HttpGet("leaseethathasxbrand/{brand}")]
        public IList<Leasing> LeaseeThatHasXBrand([FromRoute] string brand)
        {
            return logic.GetLeaseeThatHasXBrand(brand).ToList();
        }
        [HttpGet("carsleasedinbudapest")]
        public IList<CarsWithExtraInfo> CarsLeasedInBudapest()
        {
            return logic.GetCarsLeasedInBudapest().ToList();
        }
    }
}
