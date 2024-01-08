using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class PaidServicesController : ApiController
    {
        private readonly IPaidServices _ps;
        private readonly IPriceTypeMaster _ptm;
        public PaidServicesController(IPaidServices ps, IPriceTypeMaster ptm)
        {
            _ps = ps;
            _ptm = ptm;
        }

        public PaidServicesController()
        {
            _ps = new PaidServicesRepository();
            _ptm = new PriceTypeRepository();
        }

        [HttpGet]
        [Route("api/PaidServices/GetPaidServices/{BranchId}")]
        public IEnumerable<PaidServices> GetpadServices(int BranchId)
        {
            return _ps.GetPaidServices(BranchId);
        }
        [HttpPost]
        [Route("api/PaidServices/AddPaidServices/{BranchId}")]
        public bool AddPaidServices(PaidServices psEntity)
        {
            return _ps.AddPaidServices(psEntity);
        }
        [HttpDelete]
        [Route("api/PaidServices/DeletePaidServices/{BranchId}/{psId}")]
        public void DeletePaidServices(int psId)
        {
            _ps.DeletePaidService(psId);
        }
        [HttpGet]
        [Route("api/PaidServices/GetPriceType/{BranchId}")]
        public IEnumerable<PriceType> GetPriceType(int BranchId)
        {
            return _ptm.GetPriceTypes(BranchId);
        }

        [HttpGet]
        [Route("api/PaidServices/GetPaidServicesByRoomType/{roomTypeId}")]
        public IEnumerable<PaidServices> GetPaidServicesByRoomType(int roomTypeId)
        {
            return _ps.GetPaidServicesByRoomType(roomTypeId);
        }
    }
}
