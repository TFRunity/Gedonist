using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;

namespace Gedonist.Core.Interfaces
{
    public interface ICategoriesService
    {
        /// <summary>
        /// Creates category by it's binding model
        /// </summary>
        /// <param name="category"></param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Create(BindingCategory bindingcategory);

        /// <summary>
        /// Updates category
        /// </summary>
        /// <param name="category">updated category</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Update(BindingCategory category);

        /// <summary>
        /// Get all categories in the DB with all of it's data without relationships
        /// </summary>
        /// <returns>IEnumerable category</returns>
        public Task<List<Category>?> GetAll();

        /// <summary>
        ///  Get all categories in the DB with all of it's data with relationships
        /// </summary>
        /// <returns>IEnumerable category</returns>
        public Task<List<Category>?> GetAllWithRelationships();

        /// <summary>
        /// Get names of all categories (priority to use)
        /// </summary>
        /// <returns>IEnumerable string</returns>
        public Task<List<string>?> GetAllNames();

        /// <summary>
        /// Get category with all of it's object relationships
        /// </summary>
        /// <param name="id">id of this category</param>
        /// <returns>category</returns>
        public Task<Category?> GetById(int id);

        /// <summary>
        /// Get category with all of it's object relationships
        /// </summary>
        /// <param name="name">name of this category</param>
        /// <returns>category</returns>
        public Task<Category?> GetByName(string name);

        /// <summary>
        /// Cascade delete of this category with all of it's object relationships
        /// </summary>
        /// <param name="id">id of delete category</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Delete(int id);
    }
}
