using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotelBooking.Repository.Implementation
{
    public class RightsRepository : IRights
    {
        private readonly RoleRightContext _context;
        public RightsRepository(RoleRightContext context)
        {
            _context = context;
        }

        public RightsRepository()
        {
            _context = new RoleRightContext();

        }

        public int AddRights(Rights rightsEntity)
        {
            int result = -1;

            if (rightsEntity != null)
            {
                _context.Rights.Add(rightsEntity);
                _context.SaveChanges();
                result = rightsEntity.Id;
            }
            return result; 
        }

        public void DeleteRights(int rightId)
        {
            var rightsEntity = _context.Rights.Find(rightId);
            if (rightsEntity != null)
            {
                _context.Rights.Remove(rightsEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Rights> GetAllRights()
        {
            return _context.Rights.ToArray();
        }

        public IEnumerable<Rights> GetAllRightsByModuleId(int mId)
        {
            return _context.Rights.ToArray().Where(m => m.ModuleId == mId);

        }

        public Rights GetRightsById(int rightId)
        {
            return _context.Rights.Where(b => b.Id == rightId).SingleOrDefault();
        }

        public int UpdateRights(Rights rightsEntity)
        {
            int result = -1;

            if (rightsEntity != null)
            {
                //_context.Rights.Update(rightsEntity);
                _context.SaveChanges();
                result = rightsEntity.Id;
            }
            return result;
        }
    }
}
