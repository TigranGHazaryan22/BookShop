using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class OrderBooks
    {
        public int Id { get; set; }
        public Order Order { get; set; } = new Order();
        public Book Book { get; set; } = new Book();
        public int Count { get; set; }
    }
}
