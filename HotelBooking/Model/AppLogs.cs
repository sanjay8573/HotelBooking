using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace HotelBooking.Model
{
    [Table("AppLogs")]
    public class AppLogs
    {
        [Key]
        public int LogId { get; set; }
        public string LastError { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime LogDate { get; set; }

    }
}