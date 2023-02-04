namespace LegoProdavnica
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"From: {From} | Port: {Port} | Username: {UserName} | Password: {Password}";
        }
    }
}
