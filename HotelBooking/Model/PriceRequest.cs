namespace HotelBooking.Model
{
    public class PriceRequest
    {
        public int BookingId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int roomTypeId { get; set; }
        public string  CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int nOfRoom { get; set; }

    }

    public class PriceResponse
    {
        public int CostId { get; set; }
        public int roomTypeId { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public decimal Amount { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public int BookingCostId { get; set; }
        public string Description { get; set; }
        public bool isAvailable { get; set; }


    }
}
