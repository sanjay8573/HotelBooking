using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("BookingDocuments")]
    public class BookingDocuments
    {
        [Key]
        public int DocumentId { get; set; }
        public int BookingId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string PaxName { get; set; }
        public byte[] DocumentData { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        
    }
}