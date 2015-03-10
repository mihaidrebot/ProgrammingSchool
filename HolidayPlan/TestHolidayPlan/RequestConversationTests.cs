using HolidayPlan;
using NUnit.Framework;
using System;

namespace TestHolidayPlan
{
    [TestFixture]
    public class RequestConversationTests
    {
        private HolidayRequest request;

        [SetUp]
        public void SetUp()
        {
            request = new HolidayRequest();
            request.EmployeeEmail = "TestUser@TestServer.com";
            request.EmployeeName = "TestUser";
            request.ManagerEmail = "TestManager@TestServer.com";
            request.From = DateTime.Now;
            request.To = DateTime.Now.AddDays(10);
            request.Status = RequestStatus.New;
        }

        [TearDown]
        public void TearDown()
        {
            request = null;
        }

        [Test]
        public void Request_is_submited()
        {
            RequestConversation conversation = new RequestConversation(request);
            conversation.Submit();
            Assert.AreEqual(RequestStatus.Submited, conversation.Request.Status);
        }

        [Test]
        public void Request_is_approved()
        {
            request.Status = RequestStatus.Submited;
            RequestConversation conversation = new RequestConversation(request);
            conversation.Approve();
            Assert.AreEqual(RequestStatus.Approved, conversation.Request.Status);
        }

        [Test]
        public void Request_is_rejected()
        {
            request.Status = RequestStatus.Submited;
            RequestConversation conversation = new RequestConversation(request);
            conversation.Reject();
            Assert.AreEqual(RequestStatus.Rejected, conversation.Request.Status);
        }

        [Test]
        public void Request_cannot_submit_twice()
        {
            RequestConversation conversation = new RequestConversation(request);
            conversation.Submit();
            Assert.Throws<InvalidTranzitionException>(conversation.Submit);
        }

        [Test]
        public void Unsubmited_request_cannot_be_approved()
        {
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Unsubmited_request_cannot_be_rejected()
        {
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }

        [Test]
        public void Approved_request_cannot_be_approved()
        {
            request.Status = RequestStatus.Approved;
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Rejected_request_cannot_be_approved()
        {
            request.Status = RequestStatus.Rejected;
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Approve);
        }

        [Test]
        public void Approved_request_cannot_be_rejected()
        {
            request.Status = RequestStatus.Approved;
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }

        [Test]
        public void Rejected_request_cannot_be_rejected()
        {
            request.Status = RequestStatus.Rejected;
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidTranzitionException>(conversation.Reject);
        }
    }
}
