using HotelBooking.Context;
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
    public class HouseKeepingController : ApiController
    {
        private readonly IHouseKeeping _HK;
        private readonly CompanyContext _context;
        //AmenitiesRepository _Amenities = new AmenitiesRepository();

        public HouseKeepingController()
        {
            _HK = new HouseKeepingRepository(_context);
        }

        [Route("api/HouseKeeping/GetAll/{BranchId}/{roomId}")]
        [HttpGet]
        public IEnumerable<HouseKeepingData> GetAll(int BranchId,int roomId)
        {
            return _HK.GetAll(BranchId, roomId);
        }

        [Route("api/HouseKeeping/AddHouseKeeping/{BranchId}")]
        [HttpPost]
        public bool AddHouseKeeping(HouseKeepingData HouseKeepingEntity)
        {
            return _HK.addHouseKeeping(HouseKeepingEntity);
        }
    }
}
