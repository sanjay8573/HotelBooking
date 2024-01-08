using HotelBooking.Model;

using HotelBooking.Context;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Web.Http;
using System.Collections.Generic;

namespace HotelBooking.Controllers
{
   
    
    public class BranchController : ApiController
    {
       
        private readonly IBranch _Branch;
        public BranchController(IBranch br)
        {
            _Branch = br;
        }
        public BranchController()
        {
            _Branch = new BranchRepository();
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

        [HttpPost]
        [Route("api/Branch/AddBranchDetails/{companyId}")]
        public int AddBranchDetails(Branch BranchEntity)
        {
            return _Branch.AddBranch(BranchEntity);


        }
    }
}
