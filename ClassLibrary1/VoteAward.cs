using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class VoteAward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<User> Funders { get; set; } = new List<User>();
        public List<VoteOption> Votes { get; set; } = new List<VoteOption>();
        public DateTime Date { get; set; }
        public DateTime End { get; set; }
        public User Creator { get; set; } = new User();
        public List<User> VotedUsers { get; set; } = new List<User>();
    }
}
