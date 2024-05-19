using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("BookingSource")]
    public class BookingSource
    {
        [Key]
        public int BookingSourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CommissionType { get; set; }//F = Fixed P = Percentage
        public double Commission { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }

        
    }
}