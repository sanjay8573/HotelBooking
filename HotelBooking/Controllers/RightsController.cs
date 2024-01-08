using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class RightsController : ApiController
    {

        private readonly IRights _rights;
        public RightsController(IRights rights)
        {
            _rights = rights;
        }
        public RightsController()
        {
            _rights = new RightsRepository();
        }
        [HttpGet]
        [Route("api/Rights/GetRights")]
        public IEnumerable<Rights> GetRights()
        {
            return _rights.GetAllRights();
        }
        [HttpGet]
        [Route("api/Rights/GetRights/{rightId}")]
        public Rights GetRightsById(int rightId)
        {
            return _rights.GetRightsById(rightId);
        }

        [HttpGet]
        [Route("api/Rights/GetRightsByModuleId/{moduleId}")]
        public IEnumerable<Rights> GetRightsByModuleId(int moduleId)
        {
            return _rights.GetAllRightsByModuleId(moduleId);
        }



        [HttpPost]
        [Route("api/Rights/AddRight")]
        public int AddRight(Rights rifgtsEntity)
        {
            return _rights.AddRights(rifgtsEntity);


        }
    }
}
