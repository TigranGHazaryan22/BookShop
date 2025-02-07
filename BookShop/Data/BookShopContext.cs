using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext (DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public DbSet<BookShop.Models.Book> Book { get; set; } = default!;
        public DbSet<BookShop.Models.Author> Author { get; set; } = default!;
        public DbSet<BookShop.Models.User> User { get; set; } = default!;
    }
}
