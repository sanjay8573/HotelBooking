using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace HotelBooking.Repository.Implementation
{
    public class ModuleRepository : IModule
    {
        private readonly RoleRightContext _context;
        
        public ModuleRepository(RoleRightContext context)
        {
            _context = context;
        }

        public ModuleRepository()
        {
            _context = new RoleRightContext();
        }
        public int AddModule(ModuleMaster ModuleMasterEntity)
        {
            int result = -1;

            if (ModuleMasterEntity != null)
            {
                _context.ModuleMaster.Add(ModuleMasterEntity);
                _context.SaveChanges();
                result = ModuleMasterEntity.Id;
            }
            return result; ;
        }
        public void DeleteModule(int moduleId)
        {
            var ModuleMasterEntity = _context.ModuleMaster.Find(moduleId);
            if (ModuleMasterEntity != null)
            {
                _context.ModuleMaster.Remove(ModuleMasterEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ModuleMaster> GetAllModule()
        {
            return _context.ModuleMaster.ToArray();
        }

        public ModuleMaster GetModuleById(int moduleId)
        {
            return _context.ModuleMaster.Where(b => b.Id == moduleId).SingleOrDefault();
        }

        public int UpdateModule(ModuleMaster ModuleMasterEntity)
        {
            int result = -1;

            if (ModuleMasterEntity != null)
            {
                //_context.ModuleMaster.Update(ModuleMasterEntity);
                _context.SaveChanges();
                result = ModuleMasterEntity.Id;
            }
            return result;
        }
        public IEnumerable<VM_Module> GetAllModuleWithsRights()
        {
            List<VM_Module> VMM = new List<VM_Module>();
            IEnumerable<ModuleMaster> m1 = _context.ModuleMaster.ToArray();
            foreach (ModuleMaster mm in m1)
            {
                VM_Module v = new VM_Module();
                v.ModuleId = mm.Id;
                v.Name = mm.ModuleName;
                v.Description = mm.ModuleDescription;
                IEnumerable<Rights> r = _context.Rights.ToArray().Where(m => m.ModuleId == mm.Id);
                v.ModuleRights = r.ToList();
                VMM.Add(v);

            }

            return VMM.ToArray();
        }
    }
}
