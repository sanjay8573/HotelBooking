using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    
    public class ModuleController : ApiController
    {
        private readonly IModule _Module;
        private readonly IRights _Rights;
        public ModuleController(IModule module,IRights r)
        {
            _Module=module;
            _Rights = r;
        }

        public ModuleController()
        {
            _Module = new ModuleRepository();
            _Rights = new RightsRepository();
        }


        [HttpGet]
        [Route("api/Module/GetModules")]
        public IEnumerable<ModuleMaster> GetModuless()
        {
            return _Module.GetAllModule();
        }

        [HttpGet]
        [Route("api/Module/GetModule/{moduleId}")]
        public ModuleMaster GetModuleById(int moduleId)
        {
            return _Module.GetModuleById(moduleId);
        }
        [HttpPost]
        [Route("api/Module/AddModule")]
        public int AddModule(ModuleMaster ModuleMasterEntity)
        {
            return _Module.AddModule(ModuleMasterEntity);


        }
        [HttpGet]
        [Route("api/Module/GetModulesWithRights")]
        public IEnumerable<VM_Module> GetModulesWithRights()
        {
            return _Module.GetAllModuleWithsRights(); ;
        }
         
    }
}
