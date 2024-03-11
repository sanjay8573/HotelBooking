using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.Data;
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
        [Route("api/Desig/GetAllDesignation/{BranchId}")]
        public IEnumerable<Designation> GetAllDesignation(int BranchId)
        {
            return _Desig.getAllDesignation(BranchId);
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
        [HttpPost]
        [Route("api/Desig/DelDesignation")]
        public void DelDesignation(int DesigId)
        {


             _Desig.DeleteDesignation(DesigId);


        }
    }
}
