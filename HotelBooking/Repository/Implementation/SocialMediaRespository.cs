using HotelBooking.Context;
using HotelBooking.Model.SocialMedia;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class SocialMediaRespository : ISocialMedia
    {
        private readonly CompanyContext _ctx;
        public SocialMediaRespository()
        {
            _ctx = new CompanyContext();
        }
        public bool AddNewSocialmedia(SocialMediaMaster socialMediaMaster)
        {
            bool rtVal = false;
            if(socialMediaMaster == null)
            {
                _ctx.SocialMediaMaster.Add(socialMediaMaster);
                rtVal = true;
            }
            return rtVal;
        }
        public IEnumerable<SocialMediaMaster> GetAllSocialmediaMasters(int branchId)
        {
            return _ctx.SocialMediaMaster.Where(b => b.BranchId == branchId).ToArray();
        }
        public bool AddSocialMediaConfiguration(IEnumerable<SocialMediaConfiguration> smconfiguration)
        {
            bool rtVal = false;
            if (smconfiguration.Count()>0)
            {
                foreach(SocialMediaConfiguration a in smconfiguration)
                {
                    rtVal = addUpdateSocialMediaConfiguration(a);
                }
                
                
            }
            return rtVal;
        }
        private bool addUpdateSocialMediaConfiguration(SocialMediaConfiguration smconfiguration)
        {
            bool rtVal = false;
            if (smconfiguration != null)
            {
                SocialMediaConfiguration smcf = _ctx.SocialMediaConfiguration.Where(b => b.SocialmediaId == smconfiguration.SocialmediaId).SingleOrDefault();
                if (smcf != null)
                {
                    smcf.SocialmediauserName = smconfiguration.SocialmediauserName;
                    smcf.SocialmediaPassword= smconfiguration.SocialmediaPassword;
                    smcf.isActive = smconfiguration.isActive;
                }
                else
                {
                    _ctx.SocialMediaConfiguration.Add(smconfiguration);
                }
                _ctx.SaveChanges();
                rtVal = true;
            }
            return rtVal;
        }

        public BroadCastResponse BroadCast(BroadCastRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SocialMediaConfiguration> GetSocialMediaConfiguration(int branchId)
        {
            return _ctx.SocialMediaConfiguration.Where(b => b.BranchId == branchId).ToArray();
        }

        public bool RemoveSocialmedia(int socialmediaId)
        {
            bool rtVal = false;
            if(socialmediaId!=0)
            {
                SocialMediaMaster tmp = _ctx.SocialMediaMaster.Find(socialmediaId);
                if (tmp != null)
                {
                    tmp.isDeleted = true;
                    _ctx.SaveChanges();
                }
            }
            return rtVal;
        }

        public bool RemoveSocialMediaConfiguration(int socialMediaId)
        {
            throw new NotImplementedException();
        }
        public bool CreakBroadCastLink(CreateBroadCastRequest req)
        {
            bool rtVal = false;
            if(req!=null)
            {

                rtVal = false;
            }
            return rtVal;

        }
    }
}