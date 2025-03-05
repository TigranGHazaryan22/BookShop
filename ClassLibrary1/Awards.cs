namespace BookShop.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<User> Funders {  get; set; } = new List<User>();
        public Author Author { get; set; } = new Author();
        public DateTime Date { get; set; }
    }
}
