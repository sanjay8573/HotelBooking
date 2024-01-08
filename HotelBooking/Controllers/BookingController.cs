using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class BookingController : ApiController
    {
        private readonly IBooking _bk;
        public BookingController(IBooking bk)
        {
            _bk = bk;
        }
        public BookingController()
        {
            _bk = new BookingRepository();
        }

        [HttpGet]
        
        public IEnumerable<Booking> GetBookings(int BranchId)
        {
            return _bk.GetAllBooking(BranchId);
        }
        [HttpGet]
        [Route("api/Booking/GetBookingsCost/{bookingId}")]
        public IEnumerable<BookingCost> GetBookingsCost(int bookingId)
        {
            return _bk.GetAllBookingsCost(bookingId);
        }

        [HttpPost]
        [Route("api/Booking/AddBooking/{BranchId}")]
        public bool AddBooking(BookingRequest bookingEntity)
        {
            return _bk.AddBooking(bookingEntity);
        }

        [HttpPost]
        [Route("api/Booking/GetPricesForNight/{BranchId}")]
        public IEnumerable<PriceResponse> GetPricesForNight(PriceRequest PREntity)
        {
            return _bk.GetPricesForNight(PREntity);
        }

        [HttpGet]
        [Route("api/Booking/GetBooking/{bookingId}")]
        public Booking GetBooking(int bookingId)
        {
            return _bk.GetBooking(bookingId);
        }

        [HttpGet]
        [Route("api/Booking/getAvailableRooms/{roomTypeId}")]
        public IEnumerable<Room> getAvailableRooms(int roomTypeId)
        {
            return _bk.GetAllRooms(roomTypeId); 
        }

        [HttpPost]
        [Route("api/Booking/AllocateRoom/{BranchId}")]
        public bool AllocateRoom(BookedRoom bookedRoomEntity)
        {
            return _bk.AddBookedRoom(bookedRoomEntity);
        }

    }
}
