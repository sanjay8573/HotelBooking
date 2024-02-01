using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class Logger : ILogger
    {
        public void Log(string LastError, string errmessage, DateTime dt)
        {
            ErrContext _eCtx= new ErrContext();
            AppLogs al = new AppLogs();
            al.LastError = LastError;
            al.ErrorMessage=errmessage;
            al.LogDate = dt;

            _eCtx.AppLogs.Add(al);
            _eCtx.SaveChanges();

           
        }
    }
}