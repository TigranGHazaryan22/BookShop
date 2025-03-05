namespace BookShop.Models
{
    public class User : Person
    {
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
