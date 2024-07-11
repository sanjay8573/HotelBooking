using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    [Table("ReviewText")]
    public class ReviewText
    {
        [Key]
        public int ReviewId { get; set; }
        public int MasterID { get; set; }
        public string ReviewHeading { get; set; }
        [Column("ReviewText")]
        public string ReviewTextDetail { get; set; }
        public bool IsReply { get; set; }
        public DateTime DateOfReview { get; set; }
        public int repliedbyId { get; set; }
        public DateTime DateOfReply { get; set; }
        public string ReplyText { get; set; }
        public bool HasImages { get; set; }
        public Int16 CleannessRating { get; set; }
        public Int16 LocationRating { get; set; }
        public Int16 FoodRating { get; set; }
        public Int16 StaffRating { get; set; }
        public Int16 ServiceRating { get; set; }
        public Int16 RoomRating { get; set; }
        public Int16 AmenitiesRating { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }










    }
}