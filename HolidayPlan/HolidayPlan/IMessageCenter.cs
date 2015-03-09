using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HolidayPlan
{
    public interface IMessageCenter
    {
        string HrMail{get;}
        void Send(MailMessage message);
    }
}
