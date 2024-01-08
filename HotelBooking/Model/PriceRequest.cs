namespace HotelBooking.Model
{
    public class PriceRequest
    {
        public int roomTypeId { get; set; }
        public string  CheckInDate { get; set; }
        public string CheckOutDate { get; set; }

    }

    public class PriceResponse
    {
        public int roomTypeId { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }


    }
}
