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
            var ListIamge = _context.ImageMaster.Where(i => i.ImageTypeId == ImageTypeId && i.BranchId == BranchId).ToArray();
            IEnumerable<ImageMaster> aa=( from a in ListIamge
                                          select  new ImageMaster()
                                          {
                                              BranchId = a.BranchId,
                                              ImageTypeId = a.ImageTypeId,
                                              ImageContentType = a.ImageContentType,
                                              ImageData = a.ImageData,
                                              ImageId = a.ImageId,
                                              ImageName = a.ImageName,
                                              isActive = a.isActive,
                                              isDeleted = a.isDeleted,
                                              RefId = a.RefId,
                                              isMainImage = a.isMainImage,
                                              ImageToBase64String =Convert.ToBase64String(a.ImageData)
                                              

                                          }).ToArray<ImageMaster>();
                return aa;
        }
        public IEnumerable<ImageMaster> GetImages(int ImageTypeId, int BranchId,int refId)
        {
            return _context.ImageMaster.Where(i => i.ImageTypeId == ImageTypeId && i.BranchId == BranchId && i.RefId==refId).ToArray();
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
        public bool SaveImage(ImageMaster image)
        {
            bool rtnVal = false;
            try
            {
                _context.ImageMaster.Add(image);
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