using HotelBooking.Context;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class ImageRepository : IImage
    {
        private readonly CompanyContext _context;
        public ImageRepository()
        {
            _context = new CompanyContext();
        }
        public IEnumerable<ImageMaster> GetImages(int ImageTypeId, int BranchId)
        {
            return _context.ImageMaster.Where(i=>i.ImageTypeId==ImageTypeId && i.BranchId==BranchId).ToArray();
        }

        public bool SaveImages(IEnumerable<ImageMaster> images)
        {
            bool rtnVal = false;
            try
            {
                _context.ImageMaster.AddRange(images);
                _context.SaveChanges();
                rtnVal = true;

            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }
    }
}