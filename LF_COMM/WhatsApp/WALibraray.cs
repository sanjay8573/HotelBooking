using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppApi;

namespace LF_COMM.WhatsApp
{
    public  class WALibraray
    {
        public string? fromMobile { get; set; } = "9748235062";
        public string? toMobile { get; set; } = "9748235062";
        public string? Message { get; set; } = "Test Message";
        public  bool isHtmlText { get; set; }=true;

        public WALibraray() {
        
        }
        public bool Send()
        {
            bool rtnVal=false;
            WhatsAppApi.WhatsApp wa = new WhatsAppApi.WhatsApp(fromMobile, "RequiredPassword", "SD", false, false);

            try
            {
                wa.OnConnectSuccess += () =>
                {
                    wa.OnLoginSuccess += (phoneNumber, data) =>
                    {
                        wa.SendMessage(toMobile, Message);
                        rtnVal = true;
                    };

                    wa.OnLoginFailed += (data) =>
                    {
                        Message = "Login Failed" + data;
                        rtnVal = false;
                    };
                };

                wa.OnConnectFailed += (ex) =>
                {
                    Message = "Connection Failed" + ex;
                };
                wa.Connect();
            }
            catch { }
            return rtnVal;
        }

    }
}
