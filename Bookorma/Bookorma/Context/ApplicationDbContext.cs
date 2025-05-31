using System.Data.Common;
using Bookorma.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookorma.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { 
        
        }
        public DbSet<Book> books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
}
        
    }
}
