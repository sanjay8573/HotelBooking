using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("PaidServices")]
    public class PaidServices
    {
        [Key]
        public int PaidServiceId { get; set; }
        public string RoomTypeId{ get; set; }
        public int PriceTypeId { get; set; }
        public string PriceType{ get; set; }
        public decimal Price { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool isActive { get; set; }
        public int BranchId { get; set; }
    }
}
