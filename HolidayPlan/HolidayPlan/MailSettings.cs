using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace HolidayPlan
{
    class MailSettings
    {
        public string hrMail{get; private set;}
        public string Host { get; private set; }
        public int Port { get; private set; }
        public NetworkCredential Credentials { get; private set; }
        public bool EnableSsl { get; private set; }

        public readonly SmtpClient Client;

        public MailSettings()
        {
            Client = new SmtpClient();
            hrMail = ConfigurationManager.AppSettings["hrMail"];
        }
        
        public MailSettings(string hrMailAddress,SmtpClient client)
        {
            Client = client;
            hrMail = hrMailAddress;
        }

    }
}
