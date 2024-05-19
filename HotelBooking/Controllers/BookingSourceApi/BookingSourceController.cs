using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.BookingSourceApi
{
    public class BookingSourceController : ApiController
    {
        private readonly IBookingSource _bkSource;
        public BookingSourceController() { 
        _bkSource= new BookingSourceRepository();
        }

        public BookingSourceController(IBookingSource bks)
        {
            _bkSource = bks;
        }

        [Route("api/bookingSource/GetAllBookingSource/{BranchId}")]
        [HttpGet]
        public IEnumerable<BookingSource> GetAllBookingSource(int BranchId)
        {
            return _bkSource.GetAllBookingSource(BranchId);
        }
        
        [Route("api/bookingSource/AddbookingSource")]
        [HttpPost]
        public bool AddbookingSource(BookingSource bksEntity)
        {
            return _bkSource.AddBookingSource(bksEntity);
        }
        [Route("api/bookingSource/DeletebookingSource/{BranchId}/{bksId}")]
        [HttpDelete]
        public void DeletebookingSource(int BranchId, int bksId)
        {
            _bkSource.DeleteBookingSource(BranchId, bksId);
        }
    }
}
