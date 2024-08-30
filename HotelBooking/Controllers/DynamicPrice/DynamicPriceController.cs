using HotelBooking.Model.Review;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using HotelBooking.Model.DynamicPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.DynamicPrice
{
    
    public class DynamicPriceController : ApiController
    {
        private readonly IDynamicPrice _IDNP;
        public DynamicPriceController()
        {
            this._IDNP = new DynamicPriceRepository();
        }
        [Route("api/DynamicPrice/GetAllDynamicPrice")]
        [HttpGet]
        public IEnumerable<DynamicPriceModel> GetAllDynamicPrice(int branchId)
        {
            return _IDNP.GetAllDynamicPrice(branchId);
        }
        [Route("api/DynamicPrice/UpdateDynamicPrice")]
        [HttpPost]
        public bool UpdateDynamicPrice(IEnumerable<DynamicPriceModel> req)
        {
            return _IDNP.UpdateDynamicPrice(req);
        }
    }
}
