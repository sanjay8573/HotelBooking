using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using HotelBooking.Context;
using System.Data.Entity;

namespace HotelBooking.Repository.Implementation
 
{
    public class CompanyRepository : ICompany
    {
        private readonly CompanyContext _context;

        //public CompanyRepository(CompanyContext context)
        //{
        //    _context = context;
        //}
        public CompanyRepository()
        {
            _context = new CompanyContext();
        }


        public IEnumerable<Company> GetCompanies()
        {
            return _context.Company.ToList();
        }
        public Company GetCompanyById(int CompanyId)
        {
            return _context.Company.Find(CompanyId);
        }
        public int AddCompany(Company companyEntity)
        {
            int result = -1;

            if (companyEntity != null)
            {
                _context.Company.Add(companyEntity);
                _context.SaveChanges();
                result = companyEntity.Id;
            }
            return result;
        }
        public int UpdateCompany(Company CompanyEntity)
        {
            int result = -1;

            if (CompanyEntity != null)
            {
                //_context.Company.Update(CompanyEntity);
                _context.SaveChanges();
                result = CompanyEntity.Id;
            }
            return result;
        }
        public void DeleteCompany(int CompanyId)
        {
            var CompanyEntity = _context.Company.FirstOrDefault(x => x.Id == CompanyId);
            if (CompanyEntity != null) { 
            _context.Company.Remove(CompanyEntity);
            _context.SaveChanges();
            }
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

    }
}
