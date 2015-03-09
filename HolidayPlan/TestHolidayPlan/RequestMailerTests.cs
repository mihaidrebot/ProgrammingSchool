using NUnit.Framework;
using HolidayPlan;
using System.Net.Mail;
using System.Net;
using System;
using System.IO;


namespace TestHolidayPlan
{
    [TestFixture]
    class RequestMailerTests
    {
        private RequestMailer mailer;
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
            mailer = new RequestMailer();
            mailer.Setup(messageCenter);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Mailer_doesnt_work_without_setup()
        {
            RequestMailer incompleteMailer = new RequestMailer();
            RequestConversation newConversation = new RequestConversation(new HolidayRequest(), ConversationStatus.Submited);
            Assert.Throws<InvalidOperationException>(() => incompleteMailer.SendEmail(newConversation));
        }

        [Test]
        public void Mail_is_sent()
        {
            var conversation = new RequestConversation(request, ConversationStatus.Submited);
            mailer.SendEmail(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
        }

        [Test]
        public void Not_started_conversation_does_not_send_mail()
        {
            var conversation = new RequestConversation(request);
            Assert.Throws<InvalidOperationException>(()=>mailer.SendEmail(conversation));
        }        

        [Test]
        public void Submit_mail_message_is_sent()
        {
            var conversation = new RequestConversation(request, ConversationStatus.Submited);
            mailer.SendEmail(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var message = messageCenter.SentMessages[0];
            Assert.AreEqual(request.ManagerEmail, message.To[0].Address);
            Assert.AreEqual(request.EmployeeEmail, message.From.Address);
        }

        [Test]
        public void Approve_mail_message_is_sent()
        {
            var conversation = new RequestConversation(request, ConversationStatus.Approved);
            mailer.SendEmail(conversation);
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
            var conversation = new RequestConversation(request, ConversationStatus.Rejected);
            mailer.SendEmail(conversation);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var message = messageCenter.SentMessages[0];
            Assert.AreEqual(request.EmployeeEmail, message.To[0].Address);
            Assert.AreEqual(request.ManagerEmail, message.From.Address);
        }
    }
}
