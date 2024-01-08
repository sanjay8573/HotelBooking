using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;

using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace HotelBooking.Controllers
{
   
    public class DeptController : ApiController
    {

        private readonly IDept _Dept;
        public DeptController(IDept dpt)
        {
            _Dept = dpt;
        }
        public DeptController()
        {
            _Dept = new DeptRepository();
        }

        [HttpGet]
        [Route("api/Dept/GetDeptDetails/{branchid}")]
        public IEnumerable<Dept> GetDeptDetails(int branchid)
        {
            return _Dept.getDeptByBranchId(branchid);
        }

        [HttpPost]
        [Route("api/Dept/AddDeptDetails/{branchid}")]
        public int AddDeptDetails(Dept DeptEntity,int branchid)
        {
            if(DeptEntity!=null) { DeptEntity.BranchId = branchid; }  
            
            return _Dept.AddDept(DeptEntity);


        }

        [HttpGet]
        [Route("api/Dept/GetDepartment/{id}")]
        public Dept GetDeptById(int id)
        {
            return _Dept.getDeptById(id);
        }
        [HttpPost]
        [Route("api/Dept/UpdateDeptDetails/{branchid}")]
        public int UpdateDeptDetails(Dept DeptEntity)
        {
            return _Dept.UpdateDept(DeptEntity);


        }

    }
}
