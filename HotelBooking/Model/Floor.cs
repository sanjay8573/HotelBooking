using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Floors")]
    public class Floor
    {

        public int FloorId { get; set; }
        public string FloorNumber { get; set; } 
        public string Description { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int  BranchId { get; set; }



    }
}
