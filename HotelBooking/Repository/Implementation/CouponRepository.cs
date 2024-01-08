using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class CouponRepository : ICoupon
    {
        private readonly CompanyContext _context;

        public CouponRepository(CompanyContext context)
        {
            _context = context;

        }
        public CouponRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddCoupon(Coupon CouponEntity)
        {
            bool rtnVal = false;
            if (CouponEntity.CouponId > 0)
            {
                var tmpEntity = _context.Coupon.Find(CouponEntity.CouponId);
                if (tmpEntity != null)
                {
                    tmpEntity.Title = CouponEntity.Title;
                    tmpEntity.Description = CouponEntity.Description;
                    tmpEntity.CouponPeriod = CouponEntity.CouponPeriod;
                    tmpEntity.CouponCode = CouponEntity.CouponCode;
                    tmpEntity.CouponType = CouponEntity.CouponType;
                    tmpEntity.CouponValue = CouponEntity.CouponValue;
                    tmpEntity.MinimumAmount = CouponEntity.MinimumAmount;
                    tmpEntity.MaximumAmount = CouponEntity.MaximumAmount;
                    tmpEntity.Includeduser = CouponEntity.Includeduser;
                    tmpEntity.Excludeduser = CouponEntity.Excludeduser;
                    tmpEntity.IncludedRoomType = CouponEntity.IncludedRoomType;
                    tmpEntity.ExcludedRoomType = CouponEntity.ExcludedRoomType;
                    tmpEntity.PaidServices = CouponEntity.PaidServices;
                    tmpEntity.isActive = CouponEntity.isActive;
                    _context.SaveChanges();

                }

            }
            else
            {
                try
                {
                    _context.Coupon.Add(CouponEntity);
                    _context.SaveChanges();
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    return false;
                }

            }


            return rtnVal;
        }

        public CouponResponse ApplyCoupon(string couponCode="XXXX")
        {
            CouponResponse rtnVal = new CouponResponse();
            Coupon[] amtl = _context.Coupon.Select(x => new Coupon()
            {
                CouponId=x.CouponId,
                CouponCode=x.CouponCode,
                CouponValue=x.CouponValue
            }).ToArray();
            foreach(Coupon amt in amtl)
            {
                if (amt.CouponCode.ToUpper()== couponCode.ToUpper())
                {
                    rtnVal.CouponId = amt.CouponId;
                    rtnVal.CouponCode = amt.CouponCode;
                    rtnVal.CouponValue = amt.CouponValue;
                }
            }
            
           
            return rtnVal;
        }

        public void DeleteAmenities(int couponid)
        {
            try
            {
                var tmpEntity = _context.Coupon.Find(couponid);
                if (tmpEntity != null)
                {
                    _context.Coupon.Remove(tmpEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Coupon> GetAllCoupon(int BranchId)
        {
           return _context.Coupon.Where(b => b.BranchId == BranchId).ToArray();
        }
    }
}
