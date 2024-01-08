using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("BookingCost")]
    public class BookingCost
    {
        public int BookingCostId { get; set; }
        public int  BookingId { get; set; }
        public int RoomTypeId { get; set; }
        public string Date { get; set; }
        public decimal PerNightCost { get; set; }
        public string Tax { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
