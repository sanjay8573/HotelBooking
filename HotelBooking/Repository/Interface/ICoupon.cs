using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface ICoupon
    {

        IEnumerable<Coupon> GetAllCoupon(int BranchId);
        bool AddCoupon(Coupon CouponEntity);

        CouponResponse ApplyCoupon(string  couponCode);
        void DeleteAmenities(int couponid);
    }
}
