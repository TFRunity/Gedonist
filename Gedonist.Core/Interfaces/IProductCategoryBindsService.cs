

using Gedonist.Core.Models;

namespace Gedonist.Core.Interfaces
{
    public interface IProductCategoryBindsService
    {
        /// <summary>
        /// Get all Relationships without objects
        /// </summary>
        /// <returns>List of ProductCategoryBind</returns>
        public Task<List<ProductCategoryBind>?> GetAll();

        /// <summary>
        /// Creates relationship of Product and Category
        /// (for additional functionality)
        /// </summary>
        /// <param name="product_id">product to be in relationship</param>
        /// <param name="category_name">category to be in relationship</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Create(int product_id, string category_name);

        /// <summary>
        /// Deletes relationship of Product and Category
        /// </summary>
        /// <param name="product_id">product to be cancelled from rel</param>
        /// <param name="category_name">category to be cancelled from rel</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Delete(int product_id, string category_name);

        /// <summary>
        /// Set all Products to this Category
        /// </summary>
        /// <param name="product_ids">List of product ids</param>
        /// <param name="category_name">category to be modified</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Update(IList<int> product_ids, string category_name);
    }
}
