using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IStaff
    {
        IEnumerable<VM_Staff> GetStaffs(int branchId);
        Staff GetStaff(int staffId);
        int AddStaff(Staff staffEntity);
        int SetupStaffLogin(StaffLogin stfLogin);
        int ValidateLogin(LoginRequestModel model);

        IEnumerable<StaffTier> GetStaffTiers(int branchId);
        StaffLogin GetStaffLogin(int staffId);


    }
}
