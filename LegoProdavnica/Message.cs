using MimeKit;

namespace LegoProdavnica
{
    public class Message
    {
        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Message(string to, string subject, string content)
        {
            this.To = new MailboxAddress("AAA", to);
            this.Subject = subject;
            this.Body = content;
        }

        public override string ToString()
        {
            return $"To: {To.ToString()} | Subject: {Subject} | Body: {Body}";
        }
    }
}