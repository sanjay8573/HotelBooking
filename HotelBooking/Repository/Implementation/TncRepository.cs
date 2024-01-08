using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using HotelBooking.Context;
using System.Data.Entity;
using System.ComponentModel.Design;

namespace HotelBooking.Repository.Implementation
{
    
    public class TncRepository : ITnC
    {
        private readonly CompanyContext _context;
        public TncRepository(CompanyContext context)
        {
            _context = context;
        }
        public TncRepository()
        {
            _context = new CompanyContext();

        }
        public int AddTnC(TNC TnCEntity)
        {
            int result = -1;

            if (TnCEntity != null)
            {
                _context.TNC.Add(TnCEntity);
                _context.SaveChanges();
                result = TnCEntity.Id;
            }
            return result;
        }

        public void DeleteTnC(int TnCId)
        {
            var TnCEntity = _context.TNC.FirstOrDefault(x => x.Id == TnCId);
            if (TnCEntity != null) {
                _context.TNC.Remove(TnCEntity);
                _context.SaveChanges();
                }
        }

        public IEnumerable<TNC> GetTnC()
        {
            return _context.TNC.ToArray();
        }

        public TNC GetTnCByCompanyId(int CompanyId)
        {
            return _context.TNC.Where(w => w.companyId == CompanyId).SingleOrDefault();
        }

        public TNC GetTnCById(int TnCId)
        {
            return _context.TNC.Find(TnCId);
        }

        public int UpdateTnC(TNC TnCEntity)
        {
            int result = -1;

            if (TnCEntity != null)
            {
               // _context.TNC.Update(TnCEntity);
                _context.SaveChanges();
                result = TnCEntity.Id;
            }
            return result;
        }
    }
}
