using HotelBooking.Controllers.onlineAPI;
using HotelBooking.Helper;
using HotelBooking.Model.OnlineReview;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    public class OnlineController : Controller
    {
        
        // GET: Online
        public async Task<ActionResult> Review(string reqStr)
        {
            string _str = "ew0KCSJCb29raW5nSWQiOiAxLA0KCSJCcmFuY2hJZCI6MSwNCgkiR3Vlc3RJZCI6MSwNCgkiQm9va2luZ1R5cGUiOiJSb29tT25seSINCg0KfQ";


            ReviewModel _reviewResponse = await ProcessReview.RequestReview(_str);
          

            return View("Review", _reviewResponse);
        }
        public async Task<ActionResult> ReviewSubmit(ReviewModel reviewModelEntity)
        {

            ReviewModel _reviewResponse = await ProcessReview.SubmitReview(reviewModelEntity);

            return View("ReviewResult");
        }
    }
}