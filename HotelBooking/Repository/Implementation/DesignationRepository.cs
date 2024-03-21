using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class DesignationRepository : IDesignation
    {
        private readonly DeptContext _context;
        //public DesignationRepository(DeptContext context)
        //{
        //    _context = context;
        //}

        public DesignationRepository()
        {
            _context = new DeptContext();
        }
        public int AddDesignation(Designation DesignationEntity)
        {
            int result = -1;

            if (DesignationEntity != null)
            {
                if (DesignationEntity.Id > 0)
                {
                    UpdateDesignation(DesignationEntity);
                }
                else
                {
                    _context.Designation.Add(DesignationEntity);
                    _context.SaveChanges();
                }
                
                result = DesignationEntity.Id;
            }
            return result;
        }

        public void DeleteDesignation(int DesigId)
        {
            var DesignationEntity = _context.Designation.Find(DesigId);
            if (DesignationEntity != null)
            {
                DesignationEntity.isDeleted = true;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Designation> getAllDesignation(int BranchId)
        {
            return _context.Designation.Where(b=>b.BranchId== BranchId).ToArray();
        }

        public Designation getDesignationById(int DesigId)
        {
            return _context.Designation.Where(b => b.Id == DesigId).SingleOrDefault();
        }

        public int UpdateDesignation(Designation DesignationEntity)
        {
            int result = -1;

            if (DesignationEntity != null)
            {
                var tmpEntity=_context.Designation.Find(DesignationEntity.Id);
                tmpEntity.DesignationName= DesignationEntity.DesignationName;
                tmpEntity.DesignationCode= DesignationEntity.DesignationCode;
                _context.SaveChanges();
                result = tmpEntity.Id;
            }
            return result;
        }
    }
}
