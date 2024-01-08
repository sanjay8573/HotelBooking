using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IDept
    {
        IEnumerable<Dept> getAllDepartments();
        Dept getDeptById(int DeptId);
        IEnumerable<Dept> getDeptByBranchId(int BranchId);
        int AddDept(Dept DeptEntity);
        int UpdateDept(Dept DeptEntity);
        void DeleteDept(int DeptId);

    }
}
