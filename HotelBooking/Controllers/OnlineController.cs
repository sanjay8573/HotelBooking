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


        public ActionResult Review(string reqStr)
        {
            string _str = reqStr;// "ew0KCSJCb29raW5nSWQiOiAxLA0KCSJCcmFuY2hJZCI6MSwNCgkiR3Vlc3RJZCI6MSwNCgkiQm9va2luZ1R5cGUiOiJSb29tT25seSINCg0KfQ";


            //ReviewModel _reviewResponse = await ProcessReview.RequestReview(_str);
            ezyHotelController _onl = new ezyHotelController();
            string padded = reqStr.PadRight(reqStr.Length + (4 - reqStr.Length % 4) % 4, '=');
            var base64Bytes = System.Convert.FromBase64String(padded);

            string reqJSONStr = System.Text.Encoding.UTF8.GetString(base64Bytes);
            RequestJSON reqModel=JsonConvert.DeserializeObject<RequestJSON>(reqJSONStr);
            ReviewModel _reviewResponse= _onl.ReviewRequest(reqModel);

            return View("Review", _reviewResponse);
        }
        // GET: Online
        //public async Task<ActionResult> Review(string reqStr)
        //{
        //    string _str = reqStr;// "ew0KCSJCb29raW5nSWQiOiAxLA0KCSJCcmFuY2hJZCI6MSwNCgkiR3Vlc3RJZCI6MSwNCgkiQm9va2luZ1R5cGUiOiJSb29tT25seSINCg0KfQ";


        //    ReviewModel _reviewResponse = await ProcessReview.RequestReview(_str);
        
        //    return View("Review", _reviewResponse);
        //}
        //public async Task<ActionResult> ReviewSubmit(ReviewModel reviewModelEntity)
        //{

        //    ReviewModel _reviewResponse = await ProcessReview.SubmitReview(reviewModelEntity);

        //    return View("ReviewResult");
        //}
        public ActionResult ReviewSubmit(ReviewModel reviewModelEntity)
        {

            //ReviewModel _reviewResponse = await ProcessReview.SubmitReview(reviewModelEntity);
            ezyHotelController _onl = new ezyHotelController();
            ReviewResponse rr=_onl.ReviewSubmit(reviewModelEntity);
            ViewBag.RName = rr.Reviews.ReviewerName;
            return View("ReviewResult");
        }
    }
}