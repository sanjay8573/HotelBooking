using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    [Table("SocialMediaMaster")]
    public class SocialMediaMaster
    {
        [Key]
        public int SocialmediaId { get; set; }
        public string SocialmediaName { get; set; }
        public string  SocialMediaImage { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int BranchId { get; set; }
    }
}