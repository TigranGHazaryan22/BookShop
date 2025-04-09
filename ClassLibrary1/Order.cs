namespace BookShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public DateTime Date { get; set; }
        public List<OrderBooks> Counts { get; set; } = new List<OrderBooks>();
    } 
}
