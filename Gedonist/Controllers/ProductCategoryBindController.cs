using Gedonist.Core.BindingModels;
using Gedonist.Core.Interfaces;
using Gedonist.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gedonist.Controllers
{
    /// <summary>
    /// Контроллер работы с связями Product --- Category
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryBindController(IProductCategoryBindsService productCategoryBindsService) : ControllerBase
    {
        private readonly IProductCategoryBindsService productCategoryBindingService = productCategoryBindsService;

        /// <summary>
        /// Возвращает все связи Product --- Category
        /// </summary>
        /// <returns>Коллекция связей, со всеми свойствами</returns>

        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            List<ProductCategoryBind>? binds = await productCategoryBindingService.GetAll();
            return binds == null ? NotFound() : Ok(binds);
        }

        /// <summary>
        /// Создает связь Product -- Category
        /// </summary>
        /// <param name="bind">Id Категории и Id Продукта в одной модели</param>
        /// <returns>СтатусКод</returns>

        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create( [FromForm] BindingProductCategory bind)
        {
            bool result = await productCategoryBindingService.Create(bind.Product_Id, bind.Category_Name);
            return result ? Created() : Conflict("Уже есть такая связь");
        }

        /// <summary>
        /// Удаляет связь Product --- Category
        /// </summary>
        /// <param name="bind">Id Категории и Id Продукта в одной модели</param>
        /// <returns>СтатусКод</returns>

        [HttpDelete(nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] BindingProductCategory bind)
        {
            bool result = await productCategoryBindingService.Delete(bind.Product_Id, bind.Category_Name);
            return result ? Ok() : NotFound();
        }

        /// <summary>
        /// Пересоздает все отношения у категории с объектами
        /// </summary>
        /// <param name="binds">Коллекция Id Продуктов + name категории</param>
        /// <returns>СтатусКод</returns>

        [HttpPost(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update( [FromBody] UpdateCategoryProductBinds binds)
        {
            bool result = await productCategoryBindingService.Update(binds.product_Ids, binds.categoryName);
            return result ? Ok() : NotFound();
        }

    }
}
