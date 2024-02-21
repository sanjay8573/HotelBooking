using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class FloorController : ApiController
    {
        private readonly IFloor _floor;
        public FloorController(IFloor floor)
        {
            _floor = floor;
        }
        public FloorController()
        {
            _floor = new FloorRepository();
        }

        [HttpGet]
        [Route("api/Floor/GetFloors/{BranchId}")]
        public IEnumerable<Floor> GetFloors(int BranchId)
        {
            return _floor.GetAllFloors(BranchId);
        }
        [HttpPost]
        [Route("api/Floor/AddFloor/{BranchId}")]
        public bool AddFloor(Floor FloorEntity)
        {
            return _floor.AddFloor(FloorEntity);
        }

        [HttpPost]
        [Route("api/Floor/DelFloor/{BranchId}")]
        public void DelFloor(int FloorId)
        {
             _floor.DeleteFloor(FloorId);
        }
    }
}
