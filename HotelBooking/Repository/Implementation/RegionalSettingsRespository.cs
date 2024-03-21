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
    public class RegionalSettingsRespository : IRegionalSettings
    {
        private readonly CompanyContext _context;
        //public RegionalSettingsRespository(CompanyContext context)
        //{
        //    _context = context;
        //}
        public RegionalSettingsRespository()
        {
            _context = new CompanyContext();

        }
        public int AddRegionalSetting(RegionalSettings RegionalSettingsEntity)
        {
            int result = -1;

            if (RegionalSettingsEntity != null)
            {
                _context.RegionalSettings.Add(RegionalSettingsEntity);
                _context.SaveChanges();
                result = RegionalSettingsEntity.Id;
            }
            return result;
        }

        public void DeleteRegionalSetting(int RegionalSettingId)
        {
            var rs = _context.RegionalSettings.FirstOrDefault(x => x.Id == RegionalSettingId);
            if (rs != null)
            {
                _context.RegionalSettings.Remove(rs);
                _context.SaveChanges();
            }
        }

        public RegionalSettings GetRegionalSettingByCompanyId(int CompanyId)
        {
            //return _context.RegionalSettings.Find(CompanyId);
            return _context.RegionalSettings.Where(w => w.CompanyId == CompanyId).SingleOrDefault();

        }

        public RegionalSettings GetRegionalSettingById(int RegionalSettingId)
        {
            return _context.RegionalSettings.Find(RegionalSettingId);
        }

        public IEnumerable<RegionalSettings> GetRegionalSettings()
        {
            return _context.RegionalSettings.ToArray();
        }

        public int UpdateRegionalSetting(RegionalSettings RegionalSettingsEntity)
        {
            int result = -1;

            if (RegionalSettingsEntity != null)
            {
                //_context.RegionalSettings.Update(RegionalSettingsEntity);
                _context.SaveChanges();
                result = RegionalSettingsEntity.Id;
            }
            return result;
        }
    }
}
