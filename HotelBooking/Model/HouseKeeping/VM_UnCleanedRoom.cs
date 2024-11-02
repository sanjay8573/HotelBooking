using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.HouseKeeping
{
    public class VM_UnCleanedRoom
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public bool IsCleaned { get; set; }
        public int BranchId { get; set; }
    }
}