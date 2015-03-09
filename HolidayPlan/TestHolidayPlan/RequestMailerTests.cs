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
        private string dumpDir = "C:\\Temp\\";

        [SetUp]
        public void SetUp()
        {
            SetupRequest();
            SetUpMailer();

            if (!Directory.Exists(dumpDir))
            {
                Directory.CreateDirectory(dumpDir);
            }
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
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("username@gmail.com", "password");
            client.EnableSsl = false;

            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = dumpDir;//move this to tests!

            MailSettings settings = new MailSettings("hr.evilinc@gmail.com", client);

            mailer = new RequestMailer();
            mailer.Setup(settings);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Mailer_doesnt_work_without_setup()
        {
            RequestMailer incompleteMailer = new RequestMailer();
            Assert.Throws<InvalidOperationException>(()=>incompleteMailer.SendEmail(request));
        }

        [Test]
        public void Mail_is_sent()
        {
            request.Status = RequestStatus.Approved;
            mailer.SendEmail(request);
        }
    }
}
