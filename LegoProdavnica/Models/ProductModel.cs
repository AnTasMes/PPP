namespace LegoProdavnica.Models
{
    public class ProductModel
    {
        private string _id;
        private string _alternateId;
        private string _title;
        private string _description;
        private string _age;
        private string _price;
        private string _package;
        private bool _available;

        public ProductModel(string alternateId, string title, string description, string age, string price, string package, bool available)
        {
            _alternateId = alternateId;
            _title = title;
            _description = description;
            _age = age;
            _price = price;
            _package = package;
            _available = available;
        }

        public string AlternateId { get => _alternateId; set => _alternateId = value; }
        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public string Age { get => _age; set => _age = value; }
        public string Price { get => _price; set => _price = value; }
        public string Package { get => _package; set => _package = value; }
        public bool Available { get => _available; set => _available = value; }
        public string Id { get => _id; set => _id = value; }
    }
}
