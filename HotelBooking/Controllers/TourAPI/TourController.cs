using HotelBooking.Model.onlineAPI;
using HotelBooking.Model.Tour;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.TourAPI
{
    public class TourController : ApiController
    {
        private readonly ITour _itour;
        public TourController()
        {
            _itour= new TourRepository();
        }

        [Route("api/Tour/GetTourBooking")]
        [HttpPost]
        public IEnumerable<Tour> GetTourBooking(int branchId)
        {
            return _itour.getAllTourBooking(branchId);
        }
        [Route("api/Tour/createTourBooking")]
        [HttpPost]
        public TourBookingResponse createTourBooking(TourBookingRequest req)
        {
            return _itour.CreatingTourBooking(req);
        }
        [Route("api/Tour/CancellTourBooking")]
        [HttpPost]
        public bool CancellTourBooking(int tourId )
        {
            return _itour.CancellTourBooking(tourId);
        }
        [Route("api/Tour/MarkAsFullPayment")]
        [HttpPost]
        public bool MarkAsFullPayment(int tourId)
        {
            return _itour.MarkAsFullPayment(tourId);
        }

        [Route("api/Tour/GetTourBookingById")]
        [HttpPost]
        public TourBookingRequest GetTourBookingById(int branchId, int TourId)
        {
            return _itour.getTourBooking(branchId, TourId);
        }
    }
}
