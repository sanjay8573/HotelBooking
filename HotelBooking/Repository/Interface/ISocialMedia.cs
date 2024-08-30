using HotelBooking.Model.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface ISocialMedia
    {
        Boolean AddNewSocialmedia(SocialMediaMaster socialMediaMaster);
        IEnumerable<SocialMediaMaster> GetAllSocialmediaMasters(int branchId);
        Boolean RemoveSocialmedia(int socialmediaId);
        IEnumerable<SocialMediaConfiguration> GetSocialMediaConfiguration(int branchId);
        bool AddSocialMediaConfiguration(IEnumerable<SocialMediaConfiguration> smconfiguration);
       
        Boolean RemoveSocialMediaConfiguration(int  socialMediaId);
        BroadCastResponse BroadCast(BroadCastRequest request);

        bool CreakBroadCastLink(CreateBroadCastRequest req);

    }
}
