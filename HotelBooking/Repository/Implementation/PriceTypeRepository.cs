using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class PriceTypeRepository : IPriceTypeMaster
    {
        private readonly CompanyContext _context;

        //public PriceTypeRepository(CompanyContext context)
        //{
        //    _context = context;

        //}
        public PriceTypeRepository()
        {
            _context = new CompanyContext();

        }
        public IEnumerable<PriceType> GetPriceTypes(int BranchId)
        {
          return  _context.PriceType.Where(i => i.BranchId == BranchId).ToArray();
        }
    }
}
