//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System;
//using System.Net;
//using System.Net.Mail;

//namespace IMP_old.src
//{
//    public class SendEmail
//    {
//        private readonly string subject;
//        private readonly string smtpAddress;
//        private readonly string password;
//        private readonly string datBaseConnectionString;

//        public SendEmail(string subjectPrefix, EmailType emailType)
//        {
//            datBaseConnectionString = Src.DataHelper.Root.DataBaseConnection;
//            emailFromAddress = Src.DataHelper.Root.Smtpdetails.credentials.emailFromAddress;
//            smtpAddress = Src.DataHelper.Root.Smtpdetails.credentials.smtpAddress;
//            password = Src.DataHelper.Root.Smtpdetails.credentials.password;
//            subject = subjectPrefix + " " + Src.DataHelper.Root.Smtpdetails.credentials.mailsuffix + " " + DateTime.Today;
//            if (emailType == EmailType.Single)
//                emailToAddress = Src.DataHelper.Root.Smtpdetails.credentials.emailToAddress.ToArray();
//            else
//                emailToAddress = Src.DataHelper.Root.Smtpdetails.credentials.bulkEmailToAddress.ToArray();
//        }

//        private string emailFromAddress;
//        private string[] emailToAddress;

//        public void SendGridEmail(string content)
//        {

//            var apiKey = Src.DataHelper.Root.Smtpdetails.credentials.sendGridApiKey;
//            var client = new SendGridClient(apiKey);
//            var msg = new SendGridMessage()
//            {
//                From = new EmailAddress(emailFromAddress, "Online Submissions"),
//                Subject = subject,
//                HtmlContent = content
//            };

//            foreach (string email in emailToAddress)
//            {
//                msg.AddTo(new EmailAddress(email));
//            }
//            var response = (client.SendEmailAsync(msg)).Result;
//        }

//        public void Send(string content)
//        {
//            this.SendGridEmail(content);
//            return;

//            using MailMessage mail = new MailMessage
//            {
//                From = new MailAddress(emailFromAddress)
//            };
//            foreach (var email in emailToAddress)
//            {
//                mail.To.Add(email);
//            }
//            mail.Subject = subject;
//            mail.Body = content;
//            mail.IsBodyHtml = true;
//            //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment

//            using (SmtpClient smtpClient = new SmtpClient(smtpAddress))
//            {

//                smtpClient.EnableSsl = true;
//                smtpClient.UseDefaultCredentials = false;
//                smtpClient.Credentials = new NetworkCredential(emailFromAddress, password);
//                smtpClient.Port = 587;
//                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
//                smtpClient.Send(mail);
//            }
//        }
//    }
//}
