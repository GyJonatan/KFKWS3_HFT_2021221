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
        public IEnumerable<CarsWithExtraInfo> CarsForLeasee()
        {
            return logic.GetCarsForLeasee("AAA Auto");
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsOverXPrice()
        {
            return logic.GetCarsOverXPrice(8000);
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsOrderedByBudget()
        {            
             return logic.GetCarsOrderedByBudget(true);
        }

        [HttpGet]
        public IEnumerable<Leasing> LeaseeThatHasXBrand()
        {
            return logic.GetLeaseeThatHasXBrand("BMW");
        }

        [HttpGet]
        public IEnumerable<CarsWithExtraInfo> CarsLeasedInBudapest()
        {
            return logic.GetCarsLeasedInBudapest();
        }
    }
}
