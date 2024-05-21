using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataObjects;

namespace Datalayer.Models
{
    public class MyProjectContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public MyProjectContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("default");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Pages)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .IsRequired(false);

            modelBuilder.Entity<Page>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Pages)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired(true);

            modelBuilder.Entity<Reply>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Replies)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired(true);

            modelBuilder.Entity<Reply>()
                .HasOne(p => p.Page)
                .WithMany(p => p.Replies)
                .HasForeignKey(p => p.PageId)
                .IsRequired(true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
