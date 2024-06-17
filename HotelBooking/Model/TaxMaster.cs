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
        public string Name { get; set; } // Slab Name
        public string Description { get; set; }// Display Name 
        public double Value { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string appliedForName { get; set; }
        public int appliedForID { get; set; }
        public string TaxType { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }


    }
    [Table("TaxableItems")]
    public class TaxableItems
    {
        [Key]
        public int ItemTypeid { get; set; }
        public string ItemTypeName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }
    }
}