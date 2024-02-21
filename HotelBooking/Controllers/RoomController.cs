using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class RoomController : ApiController
    {
        private readonly IRoom _room;
        public RoomController(IRoom r)
        {
            _room = r;
        }
        public RoomController()
        {
            _room = new RoomRepository();
        }
        [HttpGet]
        [Route("api/Room/GetRooms/{BranchId}")]
        public IEnumerable<Room> GetRooms(int BranchId)
        {
            return _room.GetRooms(BranchId);
        }

        [HttpGet]
        [Route("api/Room/GetRoomsByRoomTypeId/{BranchId}")]
        public IEnumerable<Room> GetRoomsByRoomTypeId(int BranchId,int RoomTypeId)
        {
            return _room.GetRoomsByRoomTypeId(BranchId, RoomTypeId);
        }

        [HttpPost]
        [Route("api/Room/AddRoom/{BranchId}")]
        public bool AddRoom(Room roomEntity)
        {
            return _room.SaveRoom(roomEntity);
        }

        [HttpDelete]
        [Route("api/Room/DeleteRoom/{BranchId}/{RoomId}")]
        public void DeleteRoom(int RoomId)
        {
            _room.DeleteRoom(RoomId);
        }
    }
}
