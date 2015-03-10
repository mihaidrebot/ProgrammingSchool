using HolidayPlan;
using NUnit.Framework;
using System;


namespace TestHolidayPlan
{
    [TestFixture]
    class RequestMailerTests
    {
        private RequestMessage message;
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
        }

        private void SetUpMailer()
        {
            messageCenter = new MessageCenterMock { HrMail = "hr.evilinc@gmail.com" };
            message = new RequestMessage();
            message.Setup(messageCenter);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Mailer_doesnt_work_without_setup()
        {
            RequestMessage incompleteMailer = new RequestMessage();
            Assert.Throws<InvalidOperationException>(() => incompleteMailer.Send(new HolidayRequest(), RequestStatus.Submited));
        }

        [Test]
        public void Mail_is_sent()
        {
            message.Send(request,RequestStatus.Submited);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
        }

        [Test]
        public void Not_started_conversation_does_not_send_mail()
        {
            Assert.Throws<InvalidOperationException>(()=>message.Send(request,RequestStatus.New));
        }        

        [Test]
        public void Submit_mail_message_is_sent()
        {
            message.Send(request, RequestStatus.Submited);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var sentMsg = messageCenter.SentMessages[0];
            Assert.AreEqual(request.Manager.Email, sentMsg.To[0].Address);
            Assert.AreEqual(request.Employee.Email, sentMsg.From.Address);
        }

        [Test]
        public void Approve_mail_message_is_sent()
        {
            message.Send(request, RequestStatus.Approved);
            Assert.AreEqual(2, messageCenter.SentMessages.Count);
            var employeeMessage = messageCenter.SentMessages[0];
            Assert.AreEqual(request.Employee.Email, employeeMessage.To[0].Address);
            Assert.AreEqual(request.Manager.Email, employeeMessage.From.Address);

            var hrMessage = messageCenter.SentMessages[1];
            Assert.AreEqual(messageCenter.HrMail, hrMessage.To[0].Address);
            Assert.AreEqual(request.Manager.Email, hrMessage.From.Address);
        }

        [Test]
        public void Reject_mail_message_is_sent()
        {            
            message.Send(request, RequestStatus.Rejected);
            Assert.AreEqual(1, messageCenter.SentMessages.Count);
            var sentMsg = messageCenter.SentMessages[0];
            Assert.AreEqual(request.Employee.Email, sentMsg.To[0].Address);
            Assert.AreEqual(request.Manager.Email, sentMsg.From.Address);
        }
    }
}
