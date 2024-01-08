using HotelBooking.Model;
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
    }
}
