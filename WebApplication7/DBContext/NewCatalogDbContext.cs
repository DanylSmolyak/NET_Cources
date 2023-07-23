using Microsoft.EntityFrameworkCore;

namespace WebApplication7.DBContext
{
    public class NewCatalogDbContext : DbContext
    {
        public NewCatalogDbContext(DbContextOptions<NewCatalogDbContext> options) : base(options)
        {
        }

        public DbSet<NewCatalogfromSystemcs> NewCatalogfromSystemcs { get; set; }
    }
}
