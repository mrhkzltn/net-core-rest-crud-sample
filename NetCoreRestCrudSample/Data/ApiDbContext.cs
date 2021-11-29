using Microsoft.EntityFrameworkCore;
using NetCoreRestCrudSample.Models;

namespace NetCoreRestCrudSample.Data
{
    // dbContext provides crud operation in db.
    public class ApiDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Library> Libraries { get; set; }

        
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BookDb;Trusted_Connection=True;");
            
            base.OnConfiguring(optionsBuilder);
        }

       
        
    }
}
