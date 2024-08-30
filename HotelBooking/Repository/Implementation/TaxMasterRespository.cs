using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class TaxMasterRespository : ITaxMaster
    {
        private readonly CompanyContext _context;

        public TaxMasterRespository()
        {
            _context = new CompanyContext();
        }
        public bool AddTax(TaxMaster taxMasterEntity)
        {
            bool rtnVal = false;
            if (taxMasterEntity != null && taxMasterEntity.TaxId>0)
            {
                TaxMaster tmpTx=_context.TaxMaster.Find(taxMasterEntity.TaxId);
                if (tmpTx != null)
                {
                    tmpTx.isActive = taxMasterEntity.isActive;
                    tmpTx.Name = taxMasterEntity.Name;
                    tmpTx.Description= taxMasterEntity.Description;
                    tmpTx.RangeFrom = taxMasterEntity.RangeFrom;
                    tmpTx.RangeTo=taxMasterEntity.RangeTo;
                    tmpTx.isActive = taxMasterEntity.isActive;
                    
                    
                    rtnVal = true;
                }
            }
            else
            {
                _context.TaxMaster.Add(taxMasterEntity);
                rtnVal = true;
            }
            _context.SaveChanges();
            return rtnVal;
        }

        public bool DeleteTax(int taxId)
        {
            bool rtnVal = false;

            if (taxId > 0)
            {
               TaxMaster tmptx= _context.TaxMaster.Find(taxId);
               tmptx.isDeleted = true;
               _context.SaveChanges();
            }
            return rtnVal;
        }

        public IEnumerable<TaxMaster> GetAllTax(int branchId)
        {
           return  _context.TaxMaster.Where(b => b.BranchId == branchId).ToArray();
        }

        public TaxMaster GetTaxForBranch(int branchId)
        {
            return _context.TaxMaster.Where(b => b.BranchId == branchId).SingleOrDefault();
        }
        public IEnumerable<TaxableItems> GetAllTaxableItems(int branchId)
        {
            return _context.TaxableItems.Where(b=>b.BranchId== branchId).ToArray();
        }
        public bool MakeTaxInActive(int taxId)
        {
            bool rtnVal = false;

            if (taxId > 0)
            {
                TaxMaster tmptx = _context.TaxMaster.Find(taxId);
                tmptx.isActive = false;
                _context.SaveChanges();
            }
            return rtnVal;
        }
    }
}