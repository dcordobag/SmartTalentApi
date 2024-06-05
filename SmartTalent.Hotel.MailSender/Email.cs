namespace SmartTalent.Hotel.MailSender
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public static class Email
    {
        public static bool SendMal(EmailModel mailModel)
        {
            string senderEmail = mailModel.MailFrom;
            string senderPassword = mailModel.MailFromPassword;

            string recipientEmail = mailModel.MailTo;

            string subject = mailModel.Subject;

            string smtpServer = mailModel.SMTP;
            int smtpPort = mailModel.Port;

            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };


            MailMessage mailMessage = new(string.Concat("Smart Talent ", senderEmail), recipientEmail, subject,"Test Smart Talent");
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = CreateMailMessageBody(mailModel);

            try
            {
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static string CreateMailMessageBody(EmailModel mailModel)
        {
            string bodyOnCreate = "We can configure a template or something we want to send as a body mail";
            return bodyOnCreate;
        }
    }
    public class EmailModel
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string Subject { get; set; }
        public string MailFromPassword { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
    }
}
