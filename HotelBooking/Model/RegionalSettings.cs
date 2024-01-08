namespace HotelBooking.Model
{
    public class RegionalSettings
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string BusninessLocation { get; set; }
        public string TimeZone { get; set; }
        
        public bool isDayLightSavingEnabled { get; set; }
        public string TimeDifferent { get; set; }
        public string TimeDifferentFrom { get; set; }
        public string TimeDifferentTo { get; set; }

        public string Language { get; set; }
        public string Currency { get; set; }
        public bool  isNultiCurrencyEnabled { get; set; }
        public string DecimalDisplay { get; set; }


    }
}
