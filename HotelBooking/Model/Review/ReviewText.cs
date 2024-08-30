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
        public Byte CleannessRating { get; set; }
        public Byte LocationRating { get; set; }
        public Byte FoodRating { get; set; }
        public Byte StaffRating { get; set; }
        public Byte ServiceRating { get; set; }
        public Byte RoomRating { get; set; }
        public Byte AmenitiesRating { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }










    }
}