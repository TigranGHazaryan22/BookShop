namespace BookShop.Models
{
    public class Author : Person
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Award> Awards { get; set; } = new List<Award>();
        public double AverageScore { get; set; }
    }
}
