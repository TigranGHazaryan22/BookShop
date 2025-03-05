namespace BookShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
