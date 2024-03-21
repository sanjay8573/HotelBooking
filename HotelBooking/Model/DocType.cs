using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("DocType")]
    public class DocType
    {
        [Key]
        public int DoctypeId { get; set; }
      
        public string DocTypeName { get; set; }
        public int DocNumber { get; set; }
        public int DocCategory { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

    }
}