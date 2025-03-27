using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class VoteOption
    {
        public int Id { get; set; }
        public int Counts { get; set; }
        public Author Author { get; set; } = new Author();
    }
}
