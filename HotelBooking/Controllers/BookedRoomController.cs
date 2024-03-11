using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
   
    
    public class BookedRoomController : ApiController
    {

        private readonly IBookedRoom _IBR;
        public BookedRoomController(IBookedRoom ibr)
        {
            _IBR = ibr;
        }
        public BookedRoomController()
        {
            _IBR = new BookedRoomRepository();
        }

        [HttpGet]
        [Route("api/BookedRoom/GetAllBookedRoom/{BranchId}")]
        public IEnumerable<BookedRoom> GetAllBookedRoom(int BranchId)
        {
            return _IBR.GetAllBookedRoom(BranchId);
        }

        [HttpPost]
        [Route("api/BookedRoom/checkOut/{BranchId}")]
        public bool checkOut(CheckOutRequest coReq )
        {
            return _IBR.CheckOutRoom(coReq);
        }
        [HttpGet]
        [Route("api/BookedRoom/GetAllBookedRoomByBookingId/{BranchId}")]
        public IEnumerable<BookedRoom> GetAllBookedRoomByBookingId(int BranchId,int Bookingid)
        {
            return _IBR.GetAllBookedRoomByBookingId(BranchId,Bookingid);
        }
    }
}
