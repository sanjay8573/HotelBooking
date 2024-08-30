using HotelBooking.Model.DynamicPrice;
using HotelBooking.Model.SocialMedia;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.SocialMedia
{
    public class SocialMediaController : ApiController
    {
        private readonly ISocialMedia _ISM;
        public SocialMediaController()
        {
            this._ISM = new SocialMediaRespository();
        }
        [Route("api/SocialMedia/SocialMediaConfiguration")]
        [HttpGet]
        public IEnumerable<SocialMediaConfiguration> GetSocialMediaConfiguration(int branchId)
        {
            return _ISM.GetSocialMediaConfiguration(branchId);
        }

        [Route("api/SocialMedia/UpdateSocialMediaConfiguration")]
        [HttpPost]
        public bool UpdateSocialMediaConfiguration(IEnumerable<SocialMediaConfiguration> reg)
        {
            return _ISM.AddSocialMediaConfiguration(reg);
        }

        [Route("api/SocialMedia/GetSocialMedia")]
        [HttpGet]
        public IEnumerable<SocialMediaMaster> GetSocialMedia(int branchId)
        {
            return _ISM.GetAllSocialmediaMasters(branchId);
        }
        [Route("api/SocialMedia/AddSocialMedia")]
        [HttpPost]
        public bool AddSocialMedia(SocialMediaMaster socialMediaMaster)
        {
            return _ISM.AddNewSocialmedia(socialMediaMaster);
        }
    }
}
