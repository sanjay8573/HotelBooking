using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int floor { get; set; }
        public string FloorName { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
