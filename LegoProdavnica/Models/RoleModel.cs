namespace LegoProdavnica.Models
{
    public class RoleModel
    {
        private int _id;
        private string? _type; //Trazio mi je da dodam da je ovo nullable (Ako pravi problem, promeniti)
        private string? _comment;

        public RoleModel(string type, string comment)
        {
            Type = type;
            Comment = comment;
        }

        public int Id { get => _id; }
        public string? Type { get => _type; set => _type = value; }
        public string? Comment { get => _comment; set => _comment = value; }
    }
}
