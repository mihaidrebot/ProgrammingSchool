using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlan
{
    public class RequestConversation
    {
        public readonly HolidayRequest Request;

        public RequestConversation(HolidayRequest request)
        {
            Request = request;
        }

        public void Submit()
        {
            if(Request.Status == RequestStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Request.Status = RequestStatus.Submited;

            SendEmail();
        }        

        public void Approve()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Request.Status = RequestStatus.Approved;

            SendEmail();
        }

        public void Reject()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Request.Status = RequestStatus.Rejected;

            SendEmail();
        }

        private void ThrowInvalidOpException()
        {
            throw new InvalidOperationException();
        }

        private void SendEmail()
        {
            RequestMailer mailer = new RequestMailer();
            MailSettings mailSettings = new MailSettings();
            mailer.Setup(mailSettings);
            try
            {
                mailer.SendEmail(Request);
            }
            catch (Exception)
            {
                //nothing, settings are not good enough
            }
        }
    }
}
