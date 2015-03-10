using HolidayPlan;
using NUnit.Framework;
using System;


namespace TestHolidayPlan
{
    [TestFixture]
    class RequestMailerTests
    {
        private RequestMessage mailer;
        private HolidayRequest request;
        private MessageCenterMock messageCenter;

        [SetUp]
        public void SetUp()
        {
            SetupRequest();
            SetUpMailer();
        }
        
        private void SetupRequest()
        {
            request = new HolidayRequest();
            request.EmployeeEmail = "TestUser@TestServer.com";
            request.EmployeeName = "TestUser";
            request.ManagerEmail = "TestManager@TestServer.com";
            request.From = DateTime.Now;
            request.To = DateTime.Now.AddDays(10);
        }

        private void SetUpMailer()
        {
            messageCenter = new MessageCenterMock { HrMail = "hr.evilinc@gmail.com" };
            mailer = new RequestMessage();
            mailer.Setup(messageCenter);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Mailer_doesnt_work_without_setup()
        {
            RequestMessage incompleteMailer = new RequestMessage();
            RequestConversation newConversation = new RequestConversation(new HolidayRequest{ Status = RequestStatus.Submited});
            Assert.Throws<InvalidOperationException>(() => incompleteMailer.Send(newConversation));
        }

        [Test]
        public void Mail_is_sent()
        {
            request.Status = RequestStatus.Submited;
            var conversation = new RequestConversation(request);
            mailer.Send(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
        }

        [Test]
        public void Not_started_conversation_does_not_send_mail()
        {
            var conversation = new RequestConversation(request);
            Assert.Throws<InvalidOperationException>(()=>mailer.Send(conversation));
        }        

        [Test]
        public void Submit_mail_message_is_sent()
        {
            request.Status = RequestStatus.Submited;
            var conversation = new RequestConversation(request);
            mailer.Send(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var message = messageCenter.SentMessages[0];
            Assert.AreEqual(request.ManagerEmail, message.To[0].Address);
            Assert.AreEqual(request.EmployeeEmail, message.From.Address);
        }

        [Test]
        public void Approve_mail_message_is_sent()
        {
            request.Status = RequestStatus.Approved;
            var conversation = new RequestConversation(request);
            mailer.Send(conversation);
            Assert.AreEqual(2, messageCenter.SentMessages.Count);
            var employeeMessage = messageCenter.SentMessages[0];
            Assert.AreEqual(request.EmployeeEmail, employeeMessage.To[0].Address);
            Assert.AreEqual(request.ManagerEmail, employeeMessage.From.Address);

            var hrMessage = messageCenter.SentMessages[1];
            Assert.AreEqual(messageCenter.HrMail, hrMessage.To[0].Address);
            Assert.AreEqual(request.ManagerEmail, hrMessage.From.Address);
        }

        [Test]
        public void Reject_mail_message_is_sent()
        {
            request.Status = RequestStatus.Rejected;
            var conversation = new RequestConversation(request);
            mailer.Send(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var message = messageCenter.SentMessages[0];
            Assert.AreEqual(request.EmployeeEmail, message.To[0].Address);
            Assert.AreEqual(request.ManagerEmail, message.From.Address);
        }
    }
}
