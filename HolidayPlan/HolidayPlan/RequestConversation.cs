using System;

namespace HolidayPlan
{
    public class RequestConversation
    {
        public readonly HolidayRequest Request;
        private IMessageCenter messageCenter;
        private RequestMessage message;

        public RequestConversation(HolidayRequest request)
        {
            Request = request;
            message = new RequestMessage();
        }

        public void SetMessageCenter(IMessageCenter messageCenter)
        {
            this.messageCenter = messageCenter;
            message = new RequestMessage();
            message.Setup(messageCenter);
        }
        public void Submit()
        {
            if (Request.Status == RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Submited));
            }
            
            SendEmail(RequestStatus.Submited);
        }        

        public void Approve()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Approved));
            }

            SendEmail(RequestStatus.Approved);
        }

        public void Reject()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Rejected));
            }

            SendEmail(RequestStatus.Rejected);
        }

        private void SendEmail(RequestStatus status)
        {
            try
            {
                message.Send(Request, status);
                Request.Status = status;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("The message center was not set up.", ex);
            }            
        }
    }
}
