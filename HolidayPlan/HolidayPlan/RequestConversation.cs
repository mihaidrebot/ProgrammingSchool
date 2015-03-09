using System;

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
            RequestMessage mailer = new RequestMessage();
            MessageCenter mailSettings = new MessageCenter();
            mailer.Setup(mailSettings);
            try
            {
                mailer.Send(this);
            }
            catch (Exception)
            {
                //nothing, settings are not good enough
            }
        }
    }
}
