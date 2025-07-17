using System.Linq;
using Gedonist.Core.Models;
using Gedonist.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Gedonist.DataAccess.SeedData
{
    public static class SeedData
    {
        public static async void SeedDatabase(AppDataContext dbContext)
        {
            dbContext.Database.Migrate();
            if (dbContext.Categories.Count() == 0 && dbContext.Products.Count() == 0)
            {
                Category c1 = new Category() { Name = "Games" };
                Category c2 = new Category() { Name = "Stuff" };
                Category c3 = new Category() { Name = "Notebooks" };
                dbContext.Categories.AddRange(c1, c2, c3);
                for (int i = 0; i < 10; i++)
                {
                    Product p = new Product() { Name = i.ToString(), Description = i.ToString(), Price = 100 + i*5};
                    dbContext.Products.Add(p);
                }
                
                 await dbContext.SaveChangesAsync();
            }
        }
    }
}
