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
        [Route("api/Roles/GetRoles")]
        public IEnumerable<RoleMaster> GetRoles()
        {
            return _roles.GetAllRoles();
        }

        [HttpPost]
        [Route("api/Roles/AddRoles")]
        public int AddRoles(RoleMaster RoleMasterEntity)
        {
            return _roles.AddRoles(RoleMasterEntity);


        }
        [HttpGet]
        [Route("api/Roles/GetRoles/{roleid}")]
        public RoleMaster GetRoles(int roleid)
        {
            return _roles.GetAllModuleWithsRights(roleid);
        }
    }
}
