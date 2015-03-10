using HolidayPlan;
using NUnit.Framework;
using System;

namespace TestHolidayPlan
{
    [TestFixture]
    public class RequestConversationTests
    {
        private HolidayRequest request;
        private MessageCenterMock messageCenter;

        [SetUp]
        public void SetUp()
        {
            EmployeeMock employee = new EmployeeMock
            {
                Email = "TestUser@TestServer.com",
                Name = "Angajat Angajel"
            };
            EmployeeMock manager = new EmployeeMock
            {
                Email = "TestManager@TestServer.com",
                Name = "Manager Managerescu"
            };

            request = new HolidayRequest();
            request.Employee = employee;
            request.Manager = manager;
            request.From = DateTime.Now;
            request.To = DateTime.Now.AddDays(10);
            request.Status = RequestStatus.New;


            messageCenter = new MessageCenterMock();
        }

        [TearDown]
        public void TearDown()
        {
            request = null;
        }

        [Test]
        public void Conversation_needs_message_center()
        {
            RequestConversation conversation = new RequestConversation(request);            
            Assert.Throws<InvalidOperationException>(conversation.Submit);
        }

        [Test]
        public void Request_is_submited()
        {
            RequestConversation conversation = MakeConversation();
            conversation.Submit();
            Assert.AreEqual(RequestStatus.Submited, conversation.Request.Status);
        }

        private RequestConversation MakeConversation()
        {
            RequestConversation conversation = new RequestConversation(request);
            conversation.SetMessageCenter(messageCenter);
            return conversation;
        }

        [Test]
        public void Request_is_approved()
        {
            request.Status = RequestStatus.Submited;
            RequestConversation conversation = MakeConversation();
            conversation.Approve();
            Assert.AreEqual(RequestStatus.Approved, conversation.Request.Status);
        }

        [Test]
        public void Request_is_rejected()
        {
            request.Status = RequestStatus.Submited;
            RequestConversation conversation = MakeConversation();
            conversation.Reject();
            Assert.AreEqual(RequestStatus.Rejected, conversation.Request.Status);
        }

        [Test]
        public void Request_cannot_submit_twice()
        {
            RequestConversation conversation = MakeConversation();
            conversation.Submit();
            Assert.Throws<InvalidTranzitionException>(conversation.Submit);
        }

        [Test]
        public void Unsubmited_request_cannot_be_approved()
        {
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Unsubmited_request_cannot_be_rejected()
        {
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }

        [Test]
        public void Approved_request_cannot_be_approved()
        {
            request.Status = RequestStatus.Approved;
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Rejected_request_cannot_be_approved()
        {
            request.Status = RequestStatus.Rejected;
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Approved_request_cannot_be_rejected()
        {
            request.Status = RequestStatus.Approved;
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }

        [Test]
        public void Rejected_request_cannot_be_rejected()
        {
            request.Status = RequestStatus.Rejected;
            RequestConversation conversation = MakeConversation();
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }
    }
}
