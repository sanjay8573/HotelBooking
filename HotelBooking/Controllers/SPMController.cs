using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
   
    public class SPMController : ApiController
    {
        private readonly ISPM _ISPM;
        public SPMController(ISPM ispm)
        {
            _ISPM = ispm;
        }

        public SPMController()
        {
            _ISPM = new SPMRepository();
        }

        [HttpGet]
        [Route("api/SPM/GetAllSpecialPrice/{BranchId}")]
        public IEnumerable<SPM> GetAllSpecialPrice(int BranchId)
        {
            return _ISPM.GetSpecialPrices(BranchId);
        }

        [HttpPost]
        [Route("api/SPM/AddSpecialPrice/{BranchId}")]
        public bool AddSpecialPrice(SPM SPMEntity)
        {
            return _ISPM.AddSpecialPrice(SPMEntity);
        }
        [HttpDelete]
        [Route("api/SPM/DeleteSpecialPrice/{BranchId}/{PriceId}")]
        public void DeleteSpecialPrice(int PriceId)
        {
            _ISPM.DeleteSpecialPrice(PriceId);
        }
    }
}
