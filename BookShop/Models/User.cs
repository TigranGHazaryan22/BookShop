namespace BookShop.Models
{
    public class User : Person
    {
        public List<Review> Reviews { get; set; } = new List<Review>();
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
