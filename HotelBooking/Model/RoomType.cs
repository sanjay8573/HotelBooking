using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Room_Type")]
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Title{ get; set; }
        public string Slug { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public int BaseOccupancy { get; set; }
        public int HighOccupancy { get; set; }
        public int ExtraBed { get; set; }
        public int KidOccupancy { get; set; }
        public string Amenities { get; set; }
        public decimal BasePrice{ get; set; }
        public decimal AdditionalPersonPrice { get; set; }
        public decimal ExtraBedPrice {get; set; }
        public int BranchId { get; set; }

    }
}
