using HolidayPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestHolidayPlan
{
    class MessageCenterMock : IMessageCenter
    {
        public string HrMail { get; set; }

        public List<MailMessage> SentMessages = new List<MailMessage>();

        public void Send(MailMessage message)
        {
            SentMessages.Add(message);
        }
    }
}
