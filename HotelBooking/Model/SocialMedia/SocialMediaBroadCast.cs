using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    [Table("SocialmediaBroadCast")]
    public class SocialMediaBroadCast
    {
        [Key]
        public int SocialmediaBroadCastId { get; set; }
        public int SocialmediaId { get; set; }
        public byte[] MediaImage { get; set; }
        public string MediaText { get; set; }
        public string Medialink { get; set; }
        public DateTime BroadCastDate { get; set; }
        public bool isBroadCasted { get; set; } = false;
        public int BranchId { get; set; }
    }
}