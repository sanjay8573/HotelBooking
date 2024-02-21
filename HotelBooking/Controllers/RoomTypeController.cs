
using HotelBooking.Repository.Interface;
using HotelBooking.Model;
using System.Web.Http;
using System.Collections.Generic;
using HotelBooking.Repository.Implementation;

namespace HotelBooking.Controllers
{
    
    public class RoomTypeController : ApiController
    {
        private readonly IRoomType _rt;
        public RoomTypeController(IRoomType rt)
        {
            _rt = rt;
        }

        public RoomTypeController()
        {
            _rt = new RoomTypeRepository();
        }
        [HttpGet]
        [Route("api/RoomType/GetRoomTypes/{BranchId}")]
        public IEnumerable<RoomType> GetRoomTypes(int BranchId)
        {
            return _rt.GetRoomTypes(BranchId);
        }
        [HttpPost]
        [Route("api/RoomType/AddRoomType/{BranchId}")]
        public bool AddRoomType(RoomType RoomTypeEntity)
        {
            return _rt.AddRoomType(RoomTypeEntity);
        }


        [Route("api/RoomType/DeleteRoomtype/{BranchId}/{RoomTypeId}")]
        [HttpDelete]
        public void DeleteRoomtype(int BranchId, int RoomTypeId)
        {
            _rt.DeleteRoomType(RoomTypeId);
        }

    }
}
