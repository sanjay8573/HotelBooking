using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class BranchRepository : IBranch
    {
        private readonly CompanyContext _context;
        public BranchRepository(CompanyContext context)
        {
            _context = context;
        }
        public BranchRepository()
        {
            _context = new CompanyContext();
        }
        public int AddBranch(Branch BranchEntity)
        {
            int result = -1;

            if (BranchEntity != null)
            {
                _context.Branch.Add(BranchEntity);
                _context.SaveChanges();
                result = BranchEntity.Id;
            }
            return result;
        }

        public void DeleteBranch(int BrnachId)
        {
            var BranchEntity = _context.Branch.Find(BrnachId);
            if (BranchEntity != null)
            {
                _context.Branch.Remove(BranchEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Branch> GetBranch()
        {
            return _context.Branch.ToArray();
        }

        public IEnumerable<Branch> GetBranch(int CompanyId)
        {
             return _context.Branch.Where(b=>b.Id==CompanyId).ToArray();
        }

        public IEnumerable<Branch> GetBranchByCompanyId(int CompanyId)
        {
            var b1= _context.Branch.Where(b => b.CompanyId == CompanyId).ToArray();
            return b1.ToArray<Branch>();
        }

        public Branch GetBranchById(int BrnachId)
        {
            return _context.Branch.Find(BrnachId);
        }

        public int UpdateBranch(Branch BranchEntity)
        {
            int result = -1;

            if (BranchEntity != null)
            {
                _context.Branch.Find(BranchEntity.Id);
                _context.Entry(BranchEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = BranchEntity.Id;
            }
            return result;
        }
    }
}
