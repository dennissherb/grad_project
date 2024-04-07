using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class PageContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public PageContext(IConfiguration configuration)
        {
            _connectionString = _configuration.GetConnectionString("default");
            
        }
        public DbSet<Page> Pages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("connectionstring");
        }
    }
}
