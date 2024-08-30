using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    [Table("SocialMediaConfiguration")]
    public class SocialMediaConfiguration
    {
        [Key]
        public int SocialmediaConfigId { get; set; }
        public int SocialmediaId { get; set; }
        public string SocialmediaName { get; set; }
        public string SocialmediauserName { get; set; }
        public string SocialmediaPassword { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }= DateTime.Now;
        public int BranchId { get; set; }
    }
}