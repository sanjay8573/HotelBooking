using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("StaffTier")]
    public class StaffTier
    {
        public int StaffTierId { get; set; }
        public string StaffTierTitle { get; set; }
        public string StaffTierDescription { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }
    }
}