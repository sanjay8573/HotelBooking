using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Desig
{
    
    public class DesigController : ApiController
    {
        private readonly IDesignation _Desig;
        public DesigController(IDesignation dsg)
        {
            _Desig = dsg;
        }
        public DesigController()
        {
            _Desig = new DesignationRepository();
        }
        [HttpGet]
        [Route("api/Desig/GetAllDesignation")]
        public IEnumerable<Designation> GetAllDesignation()
        {
            return _Desig.getAllDesignation();
        }
        [HttpGet]
        [Route("api/Desig/GetDesignation/{id}")]
        public Designation GetDesignation(int id)
        {
            return _Desig.getDesignationById(id);
        }
        [HttpPost]
        [Route("api/Desig/AddDesignation")]
        public int AddDesignation(Designation DesigEntity)
        {
            

            return _Desig.AddDesignation(DesigEntity);


        }
    }
}
