namespace BookShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
        public string Genre {  get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public decimal Price { get; set; }
        public bool IsElectronicAvailable { get; set; }
        public bool IsAvailable { get; set; }
        public int AgeRestriction { get; set; }
    }
}
