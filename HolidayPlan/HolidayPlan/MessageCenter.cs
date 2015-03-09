using System.Configuration;
using System.Net.Mail;

namespace HolidayPlan
{
    class MessageCenter : IMessageCenter
    {
        public string HrMail { get; private set; }        
        private readonly SmtpClient client;

        public MessageCenter()
        {
            client = new SmtpClient();
            HrMail = ConfigurationManager.AppSettings["hrMail"];
        }
        
        public void Send(MailMessage message)
        {
            client.Send(message);
        }

    }
}
