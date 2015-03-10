using HolidayPlan;
using System.Collections.Generic;
using System.Net.Mail;

namespace TestHolidayPlan
{
    class MessageCenterMock : IMessageCenter
    {
        public string HrMail { get; set; }

        public List<MailMessage> SentMessages = new List<MailMessage>();

        public MessageCenterMock()
        {
            HrMail = "hr@TestServer.com";
        }

        public void Send(MailMessage message)
        {
            SentMessages.Add(message);
        }
    }
}
