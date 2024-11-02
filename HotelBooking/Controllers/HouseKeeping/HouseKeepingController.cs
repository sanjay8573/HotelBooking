using HotelBooking.Model;
using HotelBooking.Model.HouseKeeping;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.HouseKeeping
{
    public class HSKeepingController : ApiController
    {
        private readonly IBookedRoom _IBR;
        private readonly IHouseKeeping _HKP;
        public HSKeepingController( IBookedRoom ibr)
        {
            _IBR= ibr;
        }
        public HSKeepingController()
        {
            _IBR =  new BookedRoomRepository();
            _HKP = new HouseKeepingRepository();
        }

        [HttpGet]
        [Route("api/HouseKeeping/GetAllUnCleanedRoom/{BranchId}")]
        public IEnumerable<Model.HouseKeeping.VM_UnCleanedRoom> GetAllUnCleanedRoom(int BranchId)
        {
            return _IBR.GetAllUnCleanedRoom(BranchId);
        }
        [HttpGet]
        [Route("api/HouseKeeping/AssignToClean/{BranchId}")]
        public bool AssignToClean(HouseKeepingData _HouseKeepingData)
        {
            return _HKP.addHouseKeeping(_HouseKeepingData);
        }
    }
}
