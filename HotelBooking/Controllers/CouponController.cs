using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        [Route("api/Coupon/GetCoupons/{BranchId}")]
        public IEnumerable<Coupon> GetCoupons(int BranchId)
        {
            return _ICP.GetAllCoupon(BranchId);
        }

        [HttpPost]
        [Route("api/Coupon/SaveCoupon/{BranchId}")]
        public bool SaveCoupon(Coupon CouponEntity)
        {
            return _ICP.AddCoupon(CouponEntity);
        }

        [HttpGet]
        [Route("api/Coupon/GetCoupon/{BranchId}")]
        public Coupon GetCoupon(int BranchId, int CouponId)
        {
            return _ICP.GetAllCoupon(BranchId).Where(c=>c.CouponId== CouponId).SingleOrDefault();
        }
        [HttpPost]
        [Route("api/Coupon/DeleteCoupon/{BranchId}")]
        public void  DeleteCoupon(int BranchId, int CouponId)
        {
             _ICP.DeleteCoupon(CouponId);
        }
    }
}
