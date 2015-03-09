using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

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
