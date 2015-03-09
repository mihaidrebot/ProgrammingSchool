using System.Net.Mail;

namespace HolidayPlan
{
    public interface IMessageCenter
    {
        string HrMail{get;}
        void Send(MailMessage message);
    }
}
