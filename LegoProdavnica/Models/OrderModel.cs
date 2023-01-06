namespace LegoProdavnica.Models
{
    public class OrderModel
    {
        private string _id;
        private DateTime _created;
        private DateTime _due;
        private string _address;
        private int _userId;
        private int _productId;

        public OrderModel(DateTime created, DateTime due, string address, int userId, int productId)
        {
            Created = created;
            Due = due;
            Address = address;
            UserId = userId;
            ProductId = productId;
        }

        public string Id { get => _id; set => _id = value; }
        public DateTime Created { get => _created; set => _created = value; }
        public DateTime Due { get => _due; set => _due = value; }
        public string Address { get => _address; set => _address = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
    }
}
