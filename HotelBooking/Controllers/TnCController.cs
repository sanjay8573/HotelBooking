
using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class TnCController : ApiController
    {
        private readonly ITnC _TnC;
        public TnCController(ITnC tnc)
        {
            _TnC = tnc;
        }
        public TnCController()
        {
            _TnC = new TncRepository();
        }
        [HttpGet]
        [Route("api/TnC/GetAllTnC")]
        public IEnumerable<TNC> GetTnCs()
        {
            return _TnC.GetTnC();


        }

        [HttpGet]
        [Route("api/TnC/GetTnC/{compnayId}")]
        public TNC GetTermnConditionByCompanyId(int compnayId)
        {
            return _TnC.GetTnCByCompanyId(compnayId);


        }

        [HttpPost]
        [Route("api/TnC/AddTnC/{compnayId}")]
        public int AddTermnCondition(TNC tt)
        {
            return _TnC.AddTnC(tt);


        }
    }
}
