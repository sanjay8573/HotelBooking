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
   
    public class AmenitiesController : ApiController
    {
        private readonly IAmenities _Amenities;
        //AmenitiesRepository _Amenities = new AmenitiesRepository();

        public AmenitiesController()
        {
            _Amenities = new AmenitiesRepository();
        }
        public AmenitiesController(IAmenities im)
        {
            _Amenities = im;
        }
        [Route("api/Amenities/GetAmenities/{BranchId}")]
        [HttpGet]
        public IEnumerable<Amenities> GetAmenities(int BranchId)
        {
            return _Amenities.GetAmenities(BranchId);
        }
        //[HttpPost("AddAmenities/{BranchId}")]
        [Route("api/Amenities/AddAmenities/{BranchId}")]
        [HttpPost]
        public bool AddAmenities(Amenities AmenitiesEntity)
        {
            return _Amenities.AddAmenities(AmenitiesEntity);
        }

        //[HttpPost("updateAmenities/{BranchId}")]
        public Int64 updateAmenities(Amenities AmenitiesEntity)
        {
            return _Amenities.UpdateAmenities(AmenitiesEntity);
        }

        //[HttpDelete("DeleteAmenities/{BranchId}/{AmenitiesId}")]
        public void DeleteAmenities(int AmenitiesId)
        {
            _Amenities.DeleteAmenities(AmenitiesId);
        }
    }
}
