using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;
using Gedonist.DataAccess;
using Gedonist.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gedonist.DataAccess.Repositories
{
    public class ProductService : IProductsService
    {
        private readonly AppDataContext _context;
        public ProductService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(BindingProduct bindingProduct)
        {
            //Расширить с разными кодами ошибок, если бы было логирование - то можно было бы расширить реализацию
            bool result = await _context.Products.Where(p => p.Name == bindingProduct.Name).AnyAsync();
            if (result) return false;
            Product product = new Product() { Name = bindingProduct.Name, Description = bindingProduct.Description, Price = bindingProduct.Price };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Product? Product = await _context.Products.Where(p => p.Product_Id == id).Include(p => p.ProductCategoryBind).FirstAsync();
            if (Product == null) return false;
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>?> GetAll()
        {
            return await _context.Products.Include(p => p.ProductCategoryBind).ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> Update(Product product)
        {
            //Расширить с разными кодами ошибок
            await _context.Products.Where(p => p.Product_Id == product.Product_Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, p => product.Name)
                .SetProperty(p => p.Price, p => product.Price)
                .SetProperty(p => p.Description, p => product.Description));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
