using HotelBooking.Model.OnlineReview;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HotelBooking.Helper
{
    public class ProcessReview
    {
        public static async Task<ReviewModel> RequestReview(string reqStr)
        {
            string res = "";
            ReviewModel _roomResponse = new ReviewModel();
            try
            {
                string padded = reqStr.PadRight(reqStr.Length + (4 - reqStr.Length % 4) % 4, '=');
                var base64Bytes = System.Convert.FromBase64String(padded);

                string reqJSONStr = System.Text.Encoding.UTF8.GetString(base64Bytes);
                res = await CommonClass.PostData(reqJSONStr);
                _roomResponse = JsonConvert.DeserializeObject<ReviewModel>(res);
            }
            catch (Exception ex)
            {

            }

            return _roomResponse;
        }
        public static async Task<ReviewModel> SubmitReview(ReviewModel reqStr)
        {
            string res = "";
            ReviewModel _roomResponse = new ReviewModel();
            try
            {
                
                var reqJSONStr=JsonConvert.SerializeObject(reqStr);


                res = await CommonClass.PostReview(reqJSONStr);
                _roomResponse = JsonConvert.DeserializeObject<ReviewModel>(res);
            }
            catch (Exception ex)
            {

            }

            return _roomResponse;
        }
    }
}