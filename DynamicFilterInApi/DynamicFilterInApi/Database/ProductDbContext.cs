using DynamicExpression.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicExpression.Database
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new List<Product>
            {
                new Product() { Id = 1, Category = "TV", Name = "LB", Price = 500, IsActive = true },
                new Product() { Id = 2, Category = "Mobile", Name = "Iphone", Price = 4500, IsActive = false },
                new Product() { Id = 3, Category = "TV", Name = "Samsung", Price = 2500, IsActive = true }
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
