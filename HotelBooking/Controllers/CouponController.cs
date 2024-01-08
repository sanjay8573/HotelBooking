using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class CouponController : ApiController
    {
        private readonly ICoupon _ICP;
        public CouponController(ICoupon icp)
        {
            _ICP = icp;
        }
        public CouponController()
        {
            _ICP = new CouponRepository();
        }
        [HttpPost]
        [Route("api/Coupon/ApplyCoupon/{BranchId}/{CouponCode}")]
        public CouponResponse ApplyCoupon(string CouponCode)
        {
            return _ICP.ApplyCoupon(CouponCode);
        }
    }
}
