using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.DBContext
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
        public DbSet<Catalog> Catalog { get; set; }
    }
}
