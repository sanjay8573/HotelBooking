using LF_COMM.WhatsApp;

namespace LF_COMM
{
    public class LFCommunication
    {
        
        public bool sendMessageToWhatsApp()
        {
            WALibraray _wa = new WALibraray();
            _wa.Send();
            return true;
        }

    }
}
