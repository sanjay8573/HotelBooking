using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class DeptRepository : IDept
    {
        private readonly DeptContext _context;
        public DeptRepository(DeptContext context)
        {
            _context = context;
        }
        public DeptRepository()
        {
            _context = new DeptContext();
        }
        public int AddDept(Dept DeptEntity)
        {
            int result = -1;

            if (DeptEntity != null)
            {
                if (DeptEntity.Id > 0)
                {
                    result = UpdateDept(DeptEntity);
                }
            
            else {
                _context.Dept.Add(DeptEntity);
                _context.SaveChanges();
                result = DeptEntity.Id;
            }
                
            }
            return result;
        }

        public void DeleteDept(int DeptId)
        {
            var DeptEntity = _context.Dept.Find(DeptId);
            if (DeptEntity != null)
            {
                DeptEntity.isDeleted = true;
                
                _context.SaveChanges();
            }
        }

        public IEnumerable<Dept> getDeptByBranchId(int BranchId)
        {
            return _context.Dept.Where(b=>b.BranchId== BranchId).ToArray();
        }

        public IEnumerable<Dept> getAllDepartments()
        {
            return _context.Dept.ToArray();
        }

        public Dept getDeptById(int DeptId)
        {
            return _context.Dept.Where(b => b.Id == DeptId).SingleOrDefault();
        }

        public int UpdateDept(Dept DeptEntity)
        {
            int result = -1;

            if (DeptEntity != null)
            {
                var tmpEntity = _context.Dept.Find(DeptEntity.Id);
                tmpEntity.DepartmentName = DeptEntity.DepartmentName;
                tmpEntity.isActive = DeptEntity.isActive;
                _context.SaveChanges();
                result = DeptEntity.Id;
            }
            return result;
        }
    }
}
