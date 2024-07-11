using HotelBooking.Model;

using HotelBooking.Context;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Web.Http;
using System.Collections.Generic;
using HotelBooking.Model.EditHotel;
using HotelBooking.Model.TimeZone;
using HotelBooking.Model.Reatraurant;

namespace HotelBooking.Controllers
{
   
    
    public class BranchController : ApiController
    {
       
        private readonly IBranch _Branch;
        private readonly IImage _Image;
        public BranchController(IBranch br)
        {
            _Branch = br;
        }
        public BranchController()
        {
            _Branch = new BranchRepository();
            _Image = new ImageRepository();
        }


        [HttpGet]
        [Route("api/Branch/GetBranchDetails/{companyId}")]
        public IEnumerable<Branch> GetBranchDetails(int companyId)
        {
            return _Branch.GetBranchByCompanyId(companyId);
        }


        [HttpGet]
        [Route("api/Branch/GetBranchDetailsByBranchId/{branchId}")]
        public Branch GetBranchDetailsByBranchId(int branchId)
        {
            return _Branch.GetBranchById(branchId);
        }
        [HttpGet]
        [Route("api/Branch/GetHotelDetailsByBranchId/{branchId}")]
        public VM_EditHotel GetHotelDetailsByBranchId(int branchId)
        {
            return _Branch.GetHotelDetailById(branchId);
        }

        [HttpPost]
        [Route("api/Branch/AddBranchDetails/{companyId}")]
        public int AddBranchDetails(Branch BranchEntity)
        {
            return _Branch.AddBranch(BranchEntity);


        }
        [HttpPost]
        [Route("api/Branch/UpdateGenInfo/{BranchId}")]
        public bool UpdateGenInfo(VM_GeneralInfo genInfoEntity)
        {
            return _Branch.UpdateHotelGenInfo(genInfoEntity);


        }
        [HttpGet]
        [Route("api/Branch/GetTimeZone/{BranchId}")]
        public IEnumerable<TimeZone> GetTimeZone()
        {
            return _Branch.GetTimeZones();


        }
        [HttpPost]
        [Route("api/Branch/UpdateHotelDetails/{BranchId}")]
        public bool UpdateHotelDetails(VM_HotelDetails HDEntity)
        {
            return _Branch.UpdateHotelDetails(HDEntity);


        }
        [HttpPost]
        [Route("api/Branch/UpdateWebSiteDetails/{BranchId}")]
        public bool UpdateWebSiteDetails(VM_WebConfiguration WCEntity)
        {
            return _Branch.UpdateWebConfiguration(WCEntity);


        }
        [HttpPost]
        [Route("api/Branch/UpdateHotelAmenities/{BranchId}")]
        public bool UpdateHotelAmenities(IEnumerable<VM_Amenities> amenitiesEntity)
        {
            return _Branch.UpdateHotelAmenities(amenitiesEntity);


        }
        [HttpPost]
        [Route("api/Branch/UpdateHotelContacts/{BranchId}")]
        public bool UpdateHotelContacts(IEnumerable<VM_ContactDetails> CDEntity)
        {
            return _Branch.UpdateHotelContacts(CDEntity);


        }

        [HttpPost]
        [Route("api/Branch/UpdateHotelImages/{BranchId}")]
        public bool UpdateHotelImages(IEnumerable<VM_HotelImages> HIEntity)
        {
            return _Branch.UpdateHotelImages(HIEntity);


        }
        [HttpPost]
        [Route("api/Branch/UpdateHotelImagesNew/{BranchId}")]
        public bool UpdateHotelImagesNew(ImageMaster HIEntity)
        {
            return _Image.SaveImage(HIEntity);


        }

        [HttpGet]
        [Route("api/Branch/GetHotelImages/{companyId}")]
        public IEnumerable<ImageMaster> GetHotelImages(int BranchId)
        {
            return _Image.GetImages(0, BranchId);
        }

        [HttpPost]
        [Route("api/Branch/RemoveHotelImage/{BranchId}")]
        public bool RemoveHotelImage(int ImageId,int BranchId)
        {
            return _Branch.RemoveHotelImages(ImageId,BranchId);


        }


    }
}
