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
        public ConversationStatus Status { get; private set; }

        public RequestConversation(HolidayRequest request)
        {
            Request = request;
            Status = ConversationStatus.New;
        }

        public RequestConversation(HolidayRequest request, ConversationStatus status):this(request)
        {
            Status = status;
        }

        public void Submit()
        {
            if(Status == ConversationStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Status = ConversationStatus.Submited;

            SendEmail();
        }        

        public void Approve()
        {
            if (Status != ConversationStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Status = ConversationStatus.Approved;

            SendEmail();
        }

        public void Reject()
        {
            if (Status != ConversationStatus.Submited)
            {
                ThrowInvalidOpException();
            }
            Status = ConversationStatus.Rejected;

            SendEmail();
        }

        private void ThrowInvalidOpException()
        {
            throw new InvalidOperationException();
        }

        private void SendEmail()
        {
            RequestMailer mailer = new RequestMailer();
            MessageCenter mailSettings = new MessageCenter();
            mailer.Setup(mailSettings);
            try
            {
                mailer.SendEmail(this);
            }
            catch (Exception)
            {
                //nothing, settings are not good enough
            }
        }
    }
}
