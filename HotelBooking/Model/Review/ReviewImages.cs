using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    [Table("ReviewImages")]
    public class ReviewImages
    {
        [Key]
        public int ImageID { get; set; }
        public int MasterID { get; set; }
        public byte[] Image { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}