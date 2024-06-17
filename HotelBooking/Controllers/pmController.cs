
using HotelBooking.Model;

using HotelBooking.Context;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Web.Http;
using System.Collections.Generic;

namespace HotelBooking.Controllers
{
    
    public class pmController : ApiController
    {
        private readonly IPriceManager _IPM;
        public pmController(IPriceManager ipm)
        {
            _IPM = ipm;
        }
        public pmController()
        {
            _IPM = new PriceManageRepository();
        }
        [HttpGet]
        [Route("api/pm/GetAllPrice/{BranchId}")]
        public IEnumerable<PriceManager> GetAllPrice(int BranchId)
        {
            return _IPM.GetPrices(BranchId);
        }

        [HttpPost]
        [Route("api/pm/AddPrice/{BranchId}")]
        public string AddPrice(PriceManager PriceManagerEntity)
        {
            return _IPM.AddPrice(PriceManagerEntity);
        }
        [HttpDelete]
        [Route("api/pm/DeletePrice/{BranchId}/{PriceId}")]
        public void DeletePrice(int PriceId)
        {
            _IPM.Deleteprice(PriceId);
        }
    }
}
