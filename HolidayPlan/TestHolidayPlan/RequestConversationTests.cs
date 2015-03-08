using HolidayPlan;
using NUnit.Framework;
using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual(RequestStatus.Submited, request.Status);
        }

        [Test]
        public void Request_is_approved()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Submited;
            conversation.Approve();
            Assert.AreEqual(RequestStatus.Approved, request.Status);
        }

        [Test]
        public void Request_is_rejected()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Submited;
            conversation.Reject();
            Assert.AreEqual(RequestStatus.Rejected, request.Status);
        }

        [Test]
        public void Request_cannot_submit_twice()
        {
            RequestConversation conversation = new RequestConversation(request);
            conversation.Submit();
            Assert.Throws<InvalidOperationException>(conversation.Submit);
        }

        [Test]
        public void Unsubmited_request_cannot_be_approved()
        {
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidOperationException>(conversation.Approve);
        }

        [Test]
        public void Unsubmited_request_cannot_be_rejected()
        {
            RequestConversation conversation = new RequestConversation(request);
            Assert.Throws<InvalidOperationException>(conversation.Reject);
        }

        [Test]
        public void Approved_request_cannot_be_approved()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Approved;
            Assert.Throws<InvalidOperationException>(conversation.Approve);
        }

        [Test]
        public void Rejected_request_cannot_be_approved()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Rejected;
            Assert.Throws<InvalidOperationException>(conversation.Approve);
        }

        [Test]
        public void Approved_request_cannot_be_rejected()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Approved;
            Assert.Throws<InvalidOperationException>(conversation.Reject);
        }

        [Test]
        public void Rejected_request_cannot_be_rejected()
        {
            RequestConversation conversation = new RequestConversation(request);
            request.Status = RequestStatus.Rejected;
            Assert.Throws<InvalidOperationException>(conversation.Reject);
        }
    }
}
