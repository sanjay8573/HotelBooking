using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;


namespace HotelBooking.Controllers
{
   
    public class StaffController : ApiController
    {
        private readonly IStaff _istaff;
        private readonly IRoles _iRoles;
        public StaffController(IStaff stf,IRoles ir)
        {
            _istaff = stf;
            _iRoles = ir;
        }

        public StaffController()
        {
            _istaff = new StaffRepository();
            _iRoles = new RoleRepository();
        }
        [HttpGet]
        [Route("api/Staff/GetStaffs/{branchid}")]
        public IEnumerable<VM_Staff> GetStaffs(int branchid)
        {
            return _istaff.GetStaffs(branchid);
        }

        [HttpPost]
        [Route("api/Staff/AddStaff")]
        public int AddStaff(Staff staffEntity)
        {
            return _istaff.AddStaff(staffEntity);


        }
        [HttpPost]
        [Route("api/Staff/GetStaff")]
        public Staff GetStaff(int StaffId)
        {
            return _istaff.GetStaff(StaffId);


        }

        [HttpPost]
        [Route("api/Staff/StaffLoginSetup")]
        public int StaffLoginSetup(StaffLogin staffLoginEntity)
        {
            return _istaff.SetupStaffLogin(staffLoginEntity);


        }
        
        [HttpPost]
        
        private int validateStaff(LoginRequestModel lr)
        {
            int rtnVal= 0;
            if (lr != null)
            {
                rtnVal=_istaff.ValidateLogin(lr);
            }
            return rtnVal;
        }

        [HttpGet]
        [Route("api/Staff/GetStaffTier/{branchid}")]
        public IEnumerable<StaffTier> GetStaffTier(int branchid)
        {
            return _istaff.GetStaffTiers(branchid);
        }

    }
}
