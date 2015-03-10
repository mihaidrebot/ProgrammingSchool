using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace HolidayPlan
{
    internal class RequestMessage
    {
        bool isSetUp;
        IMessageCenter messager;


        public void Setup(IMessageCenter messageCenter)
        {
            this.messager = messageCenter;
            isSetUp = true;

        }

        private HolidayRequest Request;
        public void Send(HolidayRequest request, RequestStatus status)
        {
            if (!isSetUp)
            {
                throw new InvalidOperationException("The mail settings were not set up!");
            }

            this.Request = request;

            List<MailMessage> messages = PrepareMailMessages(status);

            SendMailMessages(messages);
        }

        private List<MailMessage> PrepareMailMessages(RequestStatus status)
        {
            List<MailMessage> messages = new List<MailMessage>();

            switch (status)
            {
                case RequestStatus.Submited:
                    messages.Add(MakeSubmitRequestMessage(Request));
                    break;
                case RequestStatus.Approved:
                    messages.Add(MakeApproveRequestMessageToEmployee(Request));
                    messages.Add(MakeApproveRequestMessageToHr(Request));

                    break;
                case RequestStatus.Rejected:
                    messages.Add(MakeRejectRequestMessage(Request));
                    break;
                default:
                    throw new InvalidOperationException("Cannot prepare message for conversation");
            }
            return messages;
        }
        
        private void SendMailMessages(List<MailMessage> messages)
        {
            foreach (MailMessage message in messages)
            {
                messager.Send(message);
            }
        }
        
        private MailMessage MakeSubmitRequestMessage(HolidayRequest request)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(request.Employee.Email);
            message.To.Add(request.Manager.Email);
            message.Subject = "Holiday request";
            message.Body = string.Format(@"Hello dear sir/madam,
Please approve my holiday request starting from {0:} until {1:}.
Thank you,
Your Best Employee", request.From, request.To);

            return message;
        }

        private MailMessage MakeApproveRequestMessageToEmployee(HolidayRequest request)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(request.Manager.Email);
            message.To.Add(request.Employee.Email);

            message.Subject = "Re: Holiday request";
            message.Body = string.Format(@"Hello dear Best Employee,
I am happy to inform you that we don't really need you between {0:} and {1:}, so take a hike.
Yours trully,
Your Manager Extraordinaire", request.From, request.To);

            return message;
        }

        private MailMessage MakeApproveRequestMessageToHr(HolidayRequest request)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(request.Manager.Email);
            message.To.Add(messager.HrMail);

            message.Subject = "Holiday request approved";
            message.Body = string.Format(@"Hello dear Best Employee,

I am happy to inform you that we don't really need you between {0:} and {1:}, so take a hike.

Yours trully,
Your Manager Extraordinaire", request.From, request.To);

            return message;
        }

        private MailMessage MakeRejectRequestMessage(HolidayRequest request)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(request.Manager.Email);
            message.To.Add(request.Employee.Email);
            message.Subject = "Re: Holiday request";
            message.Body = string.Format(@"Hello my minion,

I am happy to inform you that you are of outmost importance in my next ""Take Over the World"" atempt, that will obviously take place between {0:} and {1:}.

If it is not obvious, this means you will not go anywhere, so cancel your reservations if you were foolish enough to make any.

Yours trully,
Your Manager Extraordinaire", request.From, request.To);

            return message;
        }
    }
}
