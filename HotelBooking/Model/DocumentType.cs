using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("DocumentType")]
    public class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }
        public int BranchId { get; set; }
        public string  DocumentTypeName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        
    }
}