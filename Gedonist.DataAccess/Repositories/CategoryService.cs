using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;
using Gedonist.DataAccess;
using Gedonist.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gedonist.DataAccess.Repositories
{
    public class CategoryService : ICategoriesService
    {
        private readonly AppDataContext _context;
        public CategoryService(AppDataContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(BindingCategory bindingcategory)
        {
            //Расширить с разными кодами ошибок
            bool result = await _context.Categories.Where(c => c.Name == bindingcategory.Name).AnyAsync();
            if (result) return false;
            Category category = new Category() { Name = bindingcategory.Name };
            await _context.Categories.AddAsync(category);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Category? category = await _context.Categories.Include(c => c.ProductCategoryBinds).Where(c => c.Category_Id == id).FirstAsync();
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>?> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<string>?> GetAllNames()
        {
            return await _context.Categories.Select(c => c.Name).ToListAsync();
        }

        public async Task<List<Category>?> GetAllWithRelationships()
        {
            return await _context.Categories.Include(c => c.ProductCategoryBinds!).ThenInclude(c => c.Product).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> GetByName(string name)
        {
            return await _context.Categories.Where(c => c.Name == name).FirstAsync();
        }

        public async Task<bool> Update(Category category)
        {
            //Расширить с разными кодами
            await _context.Categories.Where(c => c.Category_Id == category.Category_Id)
                .ExecuteUpdateAsync(
                    s => s.SetProperty(c => c.Name, c => category.Name)
                );
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
