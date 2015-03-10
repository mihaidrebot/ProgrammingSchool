using HolidayPlan;
using System.Configuration;
using System.Net.Mail;

namespace Company
{
    class EmailMessageCenter : IMessageCenter
    {
        public string HrMail { get; private set; }        
        private readonly SmtpClient client;

        public EmailMessageCenter()
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
