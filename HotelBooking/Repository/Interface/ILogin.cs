﻿using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    internal interface ILogin
    {
        UserLoginResponse Login(int userid);
        LoginResponse ValidateLogin(LoginRequestModel lognReq);

    }
}
