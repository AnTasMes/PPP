using MimeKit;
using MailKit.Net.Smtp;

namespace LegoProdavnica
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public MimeMessage CreateEmailMessage(Message message)
        {
            System.Diagnostics.Debug.WriteLine("CreateEmailMessage....");
            System.Diagnostics.Debug.WriteLine(message.ToString());

            var emailMessage = new MimeMessage();

            System.Diagnostics.Debug.WriteLine("Adding parameters....");
            System.Diagnostics.Debug.WriteLine(_configuration.ToString());


            emailMessage.From.Add(new MailboxAddress(_configuration.UserName, _configuration.From));
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Body };


            System.Diagnostics.Debug.WriteLine("Adding parameters Done!");


            System.Diagnostics.Debug.WriteLine("CreateEmailMessage Done!");

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Send....");

                    client.Connect(_configuration.SmtpServer, _configuration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.UserName, _configuration.Password);
                    client.Send(mailMessage);

                    System.Diagnostics.Debug.WriteLine("Send Done!");
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception or both.
                    System.Diagnostics.Debug.WriteLine("Doslo je do greske");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
