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
        public DesignationRepository(DeptContext context)
        {
            _context = context;
        }

        public DesignationRepository()
        {
            _context = new DeptContext();
        }
        public int AddDesignation(Designation DesignationEntity)
        {
            int result = -1;

            if (DesignationEntity != null)
            {
                _context.Designation.Add(DesignationEntity);
                _context.SaveChanges();
                result = DesignationEntity.Id;
            }
            return result;
        }

        public void DeleteDesignation(int DesigId)
        {
            var DesignationEntity = _context.Designation.Find(DesigId);
            if (DesignationEntity != null)
            {
                _context.Designation.Remove(DesignationEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Designation> getAllDesignation()
        {
            return _context.Designation.ToArray();
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
               // _context.Designation.Update(DesignationEntity);
                _context.SaveChanges();
                result = DesignationEntity.Id;
            }
            return result;
        }
    }
}
