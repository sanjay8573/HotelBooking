using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("BookedRoom")]
    public class BookedRoom
    {

        public int BookedRoomId { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string CustomerName { get; set; }
        public int RoomTypeId { get; set; }
        public string  RoomTypeName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string  RoomNumber { get; set; }
        public int BranchId { get; set; }
        public bool isCheckout { get; set; }
        public bool isActive { get; set; }
        public bool isPreUpgraded { get; set; }
    }
}
