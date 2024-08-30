using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HotelBooking.Helper
{
    public class CommonClass
    {
        public static async Task<string> PostData(string jsonString1)
        {
            var ReviewUrl = ConfigurationManager.AppSettings["ReviewUrl"];
            var url = ReviewUrl +"/Review";
            
            string roomResponse = string.Empty;
            using (var httpClient = new HttpClient())
            {
                var jsonString = new StringContent(jsonString1, Encoding.UTF8, "application/json");
                //string token = jsonString;
                var response = await httpClient.PostAsync(url, jsonString);

                if (response.IsSuccessStatusCode)
                {
                    roomResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    roomResponse = ($"Error: {response.StatusCode}");
                }
            }
            return roomResponse;
        }
        public static async Task<string> PostReview(string jsonString1)
        {
            //var url = ConfigurationManager.AppSettings["BaseUrl"];
            var ReviewUrl = ConfigurationManager.AppSettings["ReviewUrl"];
            var url = ReviewUrl+"/ReviewSubmit";
            string roomResponse = string.Empty;
            using (var httpClient = new HttpClient())
            {
                var jsonString = new StringContent(jsonString1, Encoding.UTF8, "application/json");
                //string token = jsonString;
                var response = await httpClient.PostAsync(url, jsonString);

                if (response.IsSuccessStatusCode)
                {
                    roomResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    roomResponse = ($"Error: {response.StatusCode}");
                }
            }
            return roomResponse;
        }
    }
}