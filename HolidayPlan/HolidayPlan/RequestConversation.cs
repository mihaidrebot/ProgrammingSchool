using System;

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
            if (Request.Status == RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Submited));
            }
            Request.Status = RequestStatus.Submited;

            SendEmail();
        }        

        public void Approve()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Approved));
            }
            Request.Status = RequestStatus.Approved;

            SendEmail();
        }

        public void Reject()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                throw new InvalidTranzitionException(new Tranzition(Request.Status, RequestStatus.Rejected));
            }
            Request.Status = RequestStatus.Rejected;

            SendEmail();
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
