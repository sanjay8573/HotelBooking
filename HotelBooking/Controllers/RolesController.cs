using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace HotelBooking.Controllers
{


   
    public class RolesController : ApiController
    {

        private readonly IRoles _roles;
        public RolesController(IRoles rls)
        {
            _roles = rls;
        }

        public RolesController()
        {
            _roles = new RoleRepository();
        }
        [HttpGet]
        [Route("api/Roles/GetRoles/{branchId}")]
        public IEnumerable<RoleMaster> GetRoles(int branchId)
        {
            return _roles.GetAllRoles(branchId);
        }

        [HttpPost]
        [Route("api/Roles/AddRoles")]
        public int AddRoles(RoleMaster RoleMasterEntity)
        {
            return _roles.AddRoles(RoleMasterEntity);


        }
        [HttpGet]
        [Route("api/Roles/GetModuleWithsRightsRoleWise/{roleid}")]
        public RoleMaster GetModuleWithsRightsRoleWise(int roleid)
        {
            return _roles.GetAllModuleWithsRights(roleid);
        }

        //[HttpGet]
        //[Route("api/Roles/GetAllModuleWithsRights/{branchId}")]
        //public RoleMaster GetAllModuleWithsRights(int branchId)
        //{
        //    return _roles.GetAllModuleWithsRights(branchId);
        //}
    }
}
