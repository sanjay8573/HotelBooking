using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBooking.Model;

namespace HotelBooking.Repository.Interface
{
    public interface IRegionalSettings
    {
        IEnumerable<RegionalSettings> GetRegionalSettings();
        RegionalSettings GetRegionalSettingByCompanyId(int CompanyId);
        RegionalSettings GetRegionalSettingById(int RegionalSettingId);
        int AddRegionalSetting(RegionalSettings RegionalSettingsEntity);
        int UpdateRegionalSetting(RegionalSettings RegionalSettingsEntity);
        void DeleteRegionalSetting(int RegionalSettingId);
    }
}
