using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopContext : IdentityDbContext<Author>
    {
        public BookShopContext (DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<Author>().ToTable("Authors");
        }

        public DbSet<BookShop.Models.Book> Book { get; set; } = default!;
        public DbSet<BookShop.Models.Author> Author { get; set; } = default!;
        public DbSet<BookShop.Models.User> User { get; set; } = default!;
        public DbSet<BookShop.Models.Order> Orders { get; set; } = default!;
    }
}
