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
        IEnumerable<ImageMaster> GetImages(int ImageTypeId, int BranchId, int refId);
        bool SaveImages(IEnumerable<ImageMaster> images);
        bool SaveImage(ImageMaster image);
    }
}
