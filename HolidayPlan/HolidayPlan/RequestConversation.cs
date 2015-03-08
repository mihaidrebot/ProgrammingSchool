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
                Throw();
            }
            Request.Status = RequestStatus.Submited;
        }

        public void Approve()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                Throw();
            }
            Request.Status = RequestStatus.Approved;
        }

        public void Reject()
        {
            if (Request.Status != RequestStatus.Submited)
            {
                Throw();
            }
            Request.Status = RequestStatus.Rejected;
        }

        public void Throw()
        {
            throw new InvalidOperationException();
        }
    }
}
