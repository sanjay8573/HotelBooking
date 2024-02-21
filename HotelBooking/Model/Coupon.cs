using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("CouponMaster")]
    public class Coupon
    {
            [Key]
            public int CouponId { get; set; }
            public string Title { get; set; }
            public string Description{ get; set; }
           
            public byte[]  ImageData{ get; set; }
            public string CouponPeriod{ get; set; }
            public string CouponCode { get; set; }
            public string CouponType { get; set; }
            public decimal CouponValue { get; set; }
            public decimal  MinimumAmount { get; set; }
            public decimal MaximumAmount { get; set; }
            public string IncludedTier { get; set; }
            public string Excludeduser{ get; set; }
            public string IncludedRoomType { get; set; }
            public string ExcludedRoomType { get; set; }
            public string PaidServices { get; set; }
            public Int32 LimitPerUser { get; set; }
            public Int32 LimitPerCoupon { get; set; }
            public Int32 BranchId { get; set; }
            public bool isActive { get; set; }
             public bool IsDeleted { get; set; }
    }
}
