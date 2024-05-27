using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<FictionalBook> FictionalBooks { get; set; }
        public DbSet<NonFictionalBook> NonFictionalBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasDiscriminator<string>("BookType")
                .HasValue<FictionalBook>("Fictional")
                .HasValue<NonFictionalBook>("NonFictional");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews) // Assuming each book can have multiple reviews
                .HasForeignKey(r => r.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}