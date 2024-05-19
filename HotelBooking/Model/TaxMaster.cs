using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("TaxMaster")]
    public class TaxMaster
    {
        [Key]
        public int TaxId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        
    }
}