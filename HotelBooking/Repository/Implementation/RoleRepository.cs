using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class RoleRepository : IRoles
    {
        private readonly RoleRightContext _context;
        public RoleRepository(RoleRightContext context)
        {
            _context = context;
        }
        public RoleRepository()
        {
            _context = new RoleRightContext();

        }

        public int AddRoles(RoleMaster roleEntity)
        {
            int result = -1;

            if (roleEntity != null)
            {
                if (roleEntity.Id <= 0)
                {
                _context.Roles.Add(roleEntity);
                _context.SaveChanges();
                }
                else
                {
                    //_context.Roles.Update(roleEntity);
                    _context.SaveChanges();
                    result = roleEntity.Id;
                }
                
                result = roleEntity.Id;
            }
            return result; 
        }

        public void DeleteRole(int roleId)
        {
            var roleEntity = _context.Roles.Find(roleId);
            if (roleEntity != null)
            {
                _context.Roles.Remove(roleEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RoleMaster> GetAllRoles()
        {
            return _context.Roles.ToArray();
        }

        public RoleMaster GetRoleById(int roleId)
        {
            return _context.Roles.Where(b => b.Id == roleId).SingleOrDefault();
        }

        public int UpdateRole(RoleMaster roleEntity)
        {
            int result = -1;

            if (roleEntity != null)
            {
                //_context.Roles.Update(roleEntity);
                _context.SaveChanges();
                result = roleEntity.Id;
            }
            return result;
        }

        public RoleMaster GetAllModuleWithsRights(int roleId)
        {

            RoleMaster RM = new RoleMaster();
            RM = _context.Roles.Where(r=>r.Id== roleId).SingleOrDefault();
            return RM;
        }
    }
}
