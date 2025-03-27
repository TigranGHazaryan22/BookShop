using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopContext : IdentityDbContext<User>
    {
        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<Author>().ToTable("Authors");


            modelBuilder.Entity<Award>()
                .HasOne(a => a.Creator)
                .WithMany(u => u.CreatedAwards)
                .HasForeignKey("CreatorId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteAward>()
                .HasOne(v => v.Creator)
                .WithMany(u => u.CreatedPolls)
                .HasForeignKey("CreatorId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteAward>()
                .HasMany(v => v.VotedUsers)
                .WithMany(u => u.Voted)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVoteAwards",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<VoteAward>().WithMany().HasForeignKey("VoteAwardId"),
                    j => j.HasKey("UserId", "VoteAwardId")
                );

            modelBuilder.Entity<Award>()
                .HasMany(a => a.Funders)
                .WithMany(u => u.FundedAwards)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFundedAwards",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Award>().WithMany().HasForeignKey("AwardId"),
                    j => j.HasKey("UserId", "AwardId")
                );

            modelBuilder.Entity<VoteAward>()
                .HasMany(v => v.Funders)
                .WithMany(u => u.FundedPolls)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFundedVoteAwards",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<VoteAward>().WithMany().HasForeignKey("VoteAwardId"),
                    j => j.HasKey("UserId", "VoteAwardId")
                );
        }

        public DbSet<BookShop.Models.Book> Book { get; set; } = default!;
        public DbSet<BookShop.Models.Author> Author { get; set; } = default!;
        public DbSet<BookShop.Models.User> User { get; set; } = default!;
        public DbSet<BookShop.Models.Order> Orders { get; set; } = default!;
        public DbSet<BookShop.Models.Review> Review { get; set; } = default!;
        public DbSet<VoteAward> VoteAwards { get; set; }
        public DbSet<VoteOption> Options { get; set; }

    }
}
