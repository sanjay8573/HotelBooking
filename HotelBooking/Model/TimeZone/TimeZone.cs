using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.TimeZone
{
    [Table("dbo.GTimeZone")]
    public class TimeZone
    {
        [Key]
        public int TZID { get; set; }
        public string TZ_Name { get; set; }
        public string current_UTC_offset { get; set; }
    }
}