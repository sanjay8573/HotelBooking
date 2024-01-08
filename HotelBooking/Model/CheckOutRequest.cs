namespace HotelBooking.Model
{
    public class CheckOutRequest
    {
        public int BookingId { get; set; }
        public int BookedRoomId { get; set; }
        public int BranchId { get; set; }
    }
}
