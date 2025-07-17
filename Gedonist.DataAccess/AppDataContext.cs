using Gedonist.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedonist.DataAccess
{
    public class AppDataContext : DbContext 
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategoryBind> ProductCategoryBinds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(e => e.Product_Id);
            modelBuilder.Entity<Product>()
                .HasOne(e => e.ProductCategoryBind)
                .WithOne(e => e.Product)
                .HasForeignKey<ProductCategoryBind>(e => e.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Category>()
                .HasKey(e => e.Category_Id);
            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductCategoryBinds)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.Category_Id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<ProductCategoryBind>()
                .HasKey(e => new { e.Product_Id, e.Category_Id });

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
