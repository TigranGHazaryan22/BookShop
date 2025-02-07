namespace BookShop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Stars { get; set; }
        public User User { get; set; } = new User();
        public Book Book { get; set; } = new Book();
        public string Description { get; set; }
    }
}
