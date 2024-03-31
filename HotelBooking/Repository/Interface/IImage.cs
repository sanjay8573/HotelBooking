using HotelBooking.Model.Reatraurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IImage
    {
        IEnumerable<ImageMaster> GetImages(int ImageType,int BranchId);
        bool SaveImages(IEnumerable<ImageMaster> images);
    }
}
