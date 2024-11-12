using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("HALL_PARTY_MASTER")]
    public class HALL_PARTY_MASTER
    {
        [Key]
        public int HALLID { get; set; }
        public string HallName { get; set; }
        public string HallShortName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int Capasity { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public decimal RentCost { get; set; }
    }
}