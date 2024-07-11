using HotelBooking.Model;
using HotelBooking.Model.EditHotel;
using HotelBooking.Model.TimeZone;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IBranch
    {
        IEnumerable<Branch> GetBranch();
        IEnumerable<Branch> GetBranchByCompanyId(int CompanyId);
        Branch GetBranchById(int BrnachId);
        int AddBranch(Branch BranchEntity);
        int UpdateBranch(Branch BranchEntity);
        void DeleteBranch(int BrnachId);
        int AddBranchContactDetails(HotelContacts entityHotelContacts);
        IEnumerable<HotelContacts> GetBranchConatctDetails(int BranchId);
        HotelContacts getBranchContactById(int BranchConatctId);
        VM_EditHotel GetHotelDetailById(int BranchId);
        bool UpdateHotelGenInfo(VM_GeneralInfo entityGenInfo);
        bool UpdateHotelDetails(VM_HotelDetails entityHD);
        bool UpdateWebConfiguration(VM_WebConfiguration entityWC);
        bool UpdateHotelAmenities(IEnumerable<VM_Amenities> entityAmenities);
        bool UpdateHotelContacts(IEnumerable<VM_ContactDetails> entityCD);
        bool UpdateHotelImages(IEnumerable<VM_HotelImages> entityHI);
        bool RemoveHotelImages(int ImageId, int BranchId);
        IEnumerable<TimeZone> GetTimeZones();
    }
}
