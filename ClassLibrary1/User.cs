namespace BookShop.Models
{
    public class User : Person
    {
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public int Age { get; set; }
        public string Address { get; set; }
        public List<VoteAward> Voted { get; set; } = new List<VoteAward>();
        public List<VoteAward> FundedPolls { get; set; } = new List<VoteAward>();
        public List<VoteAward> CreatedPolls { get; set; } = new List<VoteAward>();
        public List<Award> CreatedAwards { get; set; } = new List<Award>();
        public List<Award> FundedAwards { get; set; } = new List<Award>();
    }
}
