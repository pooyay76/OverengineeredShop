using Catalog_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog_API.Data
{
    public class CatalogContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public CatalogContext(DbContextOptions options) : base(options)
        {
        }

    }
}
