using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    public class VM_SocialMediaConfiguration
    {
        public int SocialmediaConfigId { get; set; }
        public int SocialmediaId { get; set; }
        public string SocialmediaName { get; set; }
        public string SocialmediaLogo { get; set; }
        public string SocialmediauserName { get; set; }
        public string SocialmediaPassword { get; set; }
        public bool isActive { get; set; }
        public int BranchId { get; set; }

    }
}