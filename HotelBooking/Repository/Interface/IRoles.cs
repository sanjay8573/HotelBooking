using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IRoles
    {
        IEnumerable<RoleMaster> GetAllRoles();

        RoleMaster GetRoleById(int roleId);
        int AddRoles(RoleMaster roleEntity);
        int UpdateRole(RoleMaster roleEntity);
        void DeleteRole(int roleId);
        RoleMaster GetAllModuleWithsRights(int roleId);
    }
}
