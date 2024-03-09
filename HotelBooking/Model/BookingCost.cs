using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("BookingCost")]
    public class BookingCost
    {
        public int BookingCostId { get; set; }
        public int  BookingId { get; set; }
        public int RoomTypeId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public decimal PerNightCost { get; set; }
        public decimal OfferPrice { get; set; }
        public string Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public int CostCategory { get; set; }// 1. RoomType Cost/2. PaidService cost etc.
        public int Qty { get; set; }
    }
}
