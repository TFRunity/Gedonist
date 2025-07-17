
using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;

namespace Gedonist.Core.Interfaces
{
    public interface IProductsService
    {
        /// <summary>
        /// Get all products from db
        /// </summary>
        /// <returns>List product</returns>
        public Task<List<Product>?> GetAll();

        /// <summary>
        /// Get current product by id
        /// </summary>
        /// <param name="id">id of this product</param>
        /// <returns>product</returns>
        public Task<Product?> GetById(int id);

        //TODO add Search func

        /// <summary>
        /// Updates product's params
        /// </summary>
        /// <param name="product">updated product</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Update(Product product);

        /// <summary>
        /// Delete current product
        /// </summary>
        /// <param name="id">id of deleting product</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Delete(int id);

        /// <summary>
        /// Creating product by it's binding model
        /// </summary>
        /// <param name="bindingProduct">just like product</param>
        /// <returns>false if operation is not valid, otherwise true</returns>
        public Task<bool> Create(BindingProduct bindingProduct);
    }
}
