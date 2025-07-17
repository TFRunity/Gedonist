using Gedonist.DataAccess;
using Gedonist.Core.Interfaces;
using Gedonist.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Gedonist.DataAccess.Repositories
{
    public class ProductCategoryBindService : IProductCategoryBindsService
    {
        private readonly AppDataContext _context;
        public ProductCategoryBindService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(int product_id, string category_name)
        {
            int? category_id = await _context.Categories.Where(c => c.Name == category_name).Select(c => c.Category_Id).FirstAsync();
            if (category_id == null) return false;
            bool res = await _context.ProductCategoryBinds.Where(e => e.Product_Id == product_id && e.Category_Id == category_id).AnyAsync();
            if (res) return false;
            ProductCategoryBind bind = new ProductCategoryBind() { Product_Id = product_id, Category_Id = (int)category_id };
            await _context.ProductCategoryBinds.AddAsync(bind);
            return true;
        }

        public async Task<bool> Delete(int product_id, string category_name)
        {
            int? category_id = await _context.Categories.Where(c => c.Name == category_name).Select(c => c.Category_Id).FirstAsync();
            if (category_id == null) return false;
            ProductCategoryBind? bindToDelete = await _context.ProductCategoryBinds.Where(c => c.Product_Id == product_id && c.Category_Id == category_id).FirstAsync();
            if(bindToDelete == null) return false;
            _context.ProductCategoryBinds.Remove(bindToDelete);
            return true;
        }

        public async Task<List<ProductCategoryBind>?> GetAll()
        {
            return await _context.ProductCategoryBinds.ToListAsync();
        }

        public async Task<bool> Update(IList<int> product_ids, string category_name)
        {
            int? category_id = await _context.Categories.Where(c => c.Name == category_name).Select(c => c.Category_Id).FirstAsync();
            if (category_id == null) return false;
            List<ProductCategoryBind>? bindsToDelete = await _context.ProductCategoryBinds.Where(c => c.Category_Id == category_id).ToListAsync();
            if (bindsToDelete != null)
            {
                _context.ProductCategoryBinds.RemoveRange(bindsToDelete);
            }
            foreach (var product_id in product_ids)
            {
                ProductCategoryBind bind = new ProductCategoryBind() { Product_Id = product_id, Category_Id = (int)category_id };
                await _context.ProductCategoryBinds.AddAsync(bind);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
