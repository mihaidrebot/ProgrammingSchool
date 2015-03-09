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
        public readonly string HrMail;        
        public readonly SmtpClient Client;

        public MailSettings()
        {
            Client = new SmtpClient();
            HrMail = ConfigurationManager.AppSettings["hrMail"];
        }
        
        public MailSettings(string hrMailAddress,SmtpClient client)
        {
            Client = client;
            HrMail = hrMailAddress;
        }

    }
}
