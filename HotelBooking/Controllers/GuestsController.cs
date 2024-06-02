using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{

    public class GuestsController : ApiController
    {
        private readonly IGuests _IGuest;
        private readonly IBooking _IBK;
        public GuestsController()
        {

            _IGuest = new GuestRepository();
            _IBK= new BookingRepository();
        }
        public GuestsController(IGuests ig)
        {
            _IGuest = ig;
        }
        [Route("api/Guests/GetAllGuests/{BranchId}")]
        [HttpGet]
        public IEnumerable<Guests> GetAllGuests(int BranchId)
        {
            return _IGuest.GetAllGuest(BranchId);
        }
        [Route("api/Guests/GetAllGuests/{BranchId}/{bookingId}")]
        [HttpGet]
        public Guests GetExistingGuest(int BranchId,int bookingId)
        {
           
            Booking bk=_IBK.GetBooking(bookingId);
            Guests gt = _IGuest.GetAllGuest(BranchId).Where(g => g.GuestId == bk.GuestId).SingleOrDefault(); ;
            

            return gt;
        }

        [Route("api/Guests/GetGuest/{BranchId}/{GuestId}")]
        [HttpGet]
        public Guests GetGuest(int BranchId, int GuestId)
        {
            return _IGuest.GetGuest(GuestId);
        }
        [Route("api/Guests/AddGuest/{BranchId}")]
        [HttpPost]
        public bool AddGuest(Guests guestEntity)
        {
            return _IGuest.AddGuest(guestEntity);
        }

        [Route("api/Guests/DeleteGuest/{BranchId}/{GuestId}")]
        [HttpDelete]
        public void DeleteGuest(int BranchId, int GuestId)
        {
            _IGuest.DeleteGuest(GuestId);
        }

        [Route("api/Guests/SetAsVip/{BranchId}/{GuestId}")]
        [HttpPut]
        public void SetAsVip(int BranchId, int GuestId)
        {
            _IGuest.SetAsVip(GuestId);
        }
    }
}
