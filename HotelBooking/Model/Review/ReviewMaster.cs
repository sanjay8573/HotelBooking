using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    [Table("ReviewMaster")]
    public class ReviewMaster
    {
        [Key]
        public int MasterID { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerPhone { get; set; }
        public string ReviewerEmail { get; set;}
        public int BookingID { get; set; }
        public int propertyid { get; set; }
        public string TripType { get; set; }
        public int Clientid { get; set; }
        public bool IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}