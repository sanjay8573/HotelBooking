using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ILogin _login;
        public LoginController()
        {
            _login = new LoginRepository();
        }
        [HttpPost]
        [Route("api/Login/ValidateLogin")]
        public LoginResponse ValidateLogin(LoginRequestModel lrm)
        {
            return _login.ValidateLogin(lrm);
        }

        [HttpPost]
        [Route("api/Login/UserLogin")]
        public UserLoginResponse UserLogin(int userid)
        {
            return _login.Login(userid);
        }
    }
}
