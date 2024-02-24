using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace HotelBooking.Model
{
    [Table("Guests")]
    public class Guests { 

        [Key]
        public int GuestId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isVIP { get; set; }



    }
}