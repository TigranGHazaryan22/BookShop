using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class File
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string type { get; set; }
        public byte[] filling { get; set; }
        public string name { get; set; }
    }
}
