namespace LegoProdavnica.Models
{
    public class UserModel
    {
        private int _role;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;

        public UserModel(int role, string firstName, string lastName, string email, string password)
        {
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public int Role { get => _role; set => _role = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
