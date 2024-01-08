using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IModule
    {
        IEnumerable<ModuleMaster> GetAllModule();

        ModuleMaster GetModuleById(int moduleId);
        int AddModule(ModuleMaster ModuleMasterEntity);
        int UpdateModule(ModuleMaster ModuleMasterEntity);
        void DeleteModule(int moduleId);
        IEnumerable<VM_Module> GetAllModuleWithsRights();
    }
}
