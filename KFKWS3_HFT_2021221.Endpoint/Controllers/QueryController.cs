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

        
        [HttpGet]
        public IEnumerable<Leasing> AllDetails()
        {
            return logic.GetAllDetails();
        }

        [HttpGet]
        public IEnumerable<AveragesResult> BrandAverages()
        {
            return logic.GetBrandAverages();
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsForLeasee([FromRoute] string name)
        {
            return logic.GetCarsForLeasee(name);
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsOverXPrice([FromRoute] int price)
        {
            return logic.GetCarsOverXPrice(price);
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsOrderedByBudget([FromRoute] string order)
        {
            return order == "asc" ?
                logic.GetCarsOrderedByBudget(true) :
                logic.GetCarsOrderedByBudget(false);
        }

        [HttpGet]
        public IEnumerable<Leasing> LeaseeThatHasXBrand([FromRoute] string brand)
        {
            return logic.GetLeaseeThatHasXBrand(brand);
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsLeasedInBudapest()
        {
            return logic.GetCarsLeasedInBudapest();
        }
    }
}
