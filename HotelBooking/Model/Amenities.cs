using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelBooking.Model
{
    [Table("Amenities")]
    public class Amenities
    {
        public int BranchId { get; set; }
        public int AmenitiesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public byte[] imageData { get; set; }
        public bool IsActive { get; set; }=false;
        public DateTime? DateCreated { get; set; }
        
       
    }
}
