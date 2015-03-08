using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HolidayPlan
{
    class MailSettings
    {
        public string hrMail{get; private set;}
        public string Host { get; private set; }
        public int Port { get; private set; }
        public NetworkCredential Credentials { get; private set; }
        public bool EnableSsl { get; private set; }

        private MailSettings()
        {
        }

        public static MailSettings GetSettings()
        {
            return new MailSettings()
            {

                hrMail = "hr@gmail.com",
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("username@gmail.com", "password"),
                EnableSsl = true,
            };//TODO: read these from settings
        }


    }
}
