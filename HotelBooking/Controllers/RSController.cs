using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
   
    public class RSController : ApiController
    {
        private readonly IRegionalSettings _RegionalSettingsRepository;

        public RSController(IRegionalSettings IRS)
        {
            _RegionalSettingsRepository = IRS;
        }

        public RSController()
        {
            _RegionalSettingsRepository = new RegionalSettingsRespository();
        }

        [HttpGet]
        [Route("api/RS/GetRegionalSettings")]
        public IEnumerable<RegionalSettings> GetCompanyDetails()
        {
            return _RegionalSettingsRepository.GetRegionalSettings();


        }
        [HttpGet]
        [Route("api/RS/GetRegionalSettings/{compnayId}")]
        public RegionalSettings GetRegionalSettings(int compnayId)
        {
            return _RegionalSettingsRepository.GetRegionalSettingByCompanyId(compnayId);


        }

        [HttpPost]
        [Route("api/RS/AddRegionalSettings/{compnayId}")]
        public int AddRegionalSettings(RegionalSettings RSEntity)
        {
            return _RegionalSettingsRepository.AddRegionalSetting(RSEntity);


        }

    }
}
