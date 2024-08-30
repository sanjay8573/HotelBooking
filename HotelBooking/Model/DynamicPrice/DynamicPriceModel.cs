using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.DynamicPrice
{
    [Table("DynamicPrice")]
    public class DynamicPriceModel
    {
        [Key]
        public int DynamicPriceId { get; set; }
        public int BranchId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int Slab1 { get; set; }
        public int Slab1_Thresold { get; set; }
        public int Slab2 { get; set; }

        public int Slab2_Thresold { get; set; }

        public int Slab3 { get; set; }

        public int Slab3_Thresold { get; set; }

        public bool isFixed { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public bool isActive { get; set; }=true;
        public bool isDeleted { get; set; } = false;
    }
}