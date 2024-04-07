using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Account>().ToTable("accounts");
        //    modelBuilder.Entity<Account>().HasKey(a => a.Id);
        //    modelBuilder.Entity<Account>().Property(a => a.Email).IsRequired().HasMaxLength(255);
        //    modelBuilder.Entity<Account>().HasIndex(a => a.Email).IsUnique();

        //    // Map entity to table
        //    modelBuilder.Entity<Product>().ToTable("products");

        //    // Configure primary key
        //    modelBuilder.Entity<Product>().HasKey(p => p.Id);

        //    // Configure properties
        //    modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
        //    modelBuilder.Entity<Product>().Property(p => p.Company).IsRequired();
        //    modelBuilder.Entity<Product>().Property(p => p.Category).IsRequired();
        //    modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)");
        //    modelBuilder.Entity<Product>().Property(p => p.Image).HasColumnType("longblob");

        //    // Configure nullable property
        //    modelBuilder.Entity<Product>().Property(p => p.Image).IsRequired(false);


        //    modelBuilder.Entity<Page>().ToTable("pages");
        //    modelBuilder.Entity<Page>().HasKey(p => p.Id);
        //    modelBuilder.Entity<Page>().Property(p => p.Type).IsRequired();
        //    modelBuilder.Entity<Page>().Property(p => p.AuthorId).IsRequired();
        //    modelBuilder.Entity<Page>().Property(p => p.ProductId).HasColumnName("fkproducts_id");
        //    modelBuilder.Entity<Page>().Property(p => p.Tags).HasColumnName("pages_tags");
        //    modelBuilder.Entity<Page>().Property(p => p.Content).HasColumnName("pages_content");

        //    // Define foreign key relationship
        //    modelBuilder.Entity<Page>()
        //        .HasOne(p => p.Product)
        //        .WithMany()
        //        .HasForeignKey(p => p.ProductId)
        //        .OnDelete(DeleteBehavior.Restrict); // or Cascade if desired

        //    modelBuilder.Entity<Page>()
        //        .HasOne(p => p.Author)
        //        .WithMany()
        //        .HasForeignKey(p => p.AuthorId)
        //        .OnDelete(DeleteBehavior.Restrict); // or Cascade if desired
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
