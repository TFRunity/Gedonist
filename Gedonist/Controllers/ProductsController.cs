using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;
using Gedonist.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gedonist.Controllers
{
    /// <summary>
    /// Контроллер работы с продуктами
    /// </summary>
     
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsService productsService) : ControllerBase
    {
        private readonly IProductsService productsService = productsService;

        /// <summary>
        /// Создание продукта с базовыми свойствами
        /// </summary>
        /// <param name="bindingProduct">базовые параметры продукта в модели</param>
        /// <returns>СтатусКод</returns>

        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] BindingProduct bindingProduct)
        {
            bool result = await productsService.Create(bindingProduct);
            return result ? Created() : NotFound();
        }


        /// <summary>
        /// Возвращает продукт по id
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <returns>Получение по id</returns>

        [HttpGet(nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Product? product = await productsService.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Вовзращает все продукты без связей
        /// </summary>
        /// <returns>Коллекцию продуктов</returns>

        [HttpGet(nameof(GetAll))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            List<Product>? products = await productsService.GetAll();
            return products == null ? NotFound() : Ok(products);
        }

        /// <summary>
        /// Каскадное удаление продукта
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <returns>СтатусКод</returns>

        [HttpDelete(nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete( [FromBody] int id)
        {
            bool result = await productsService.Delete(id);
            return result ? Ok() : NotFound();
        }

        /// <summary>
        /// Обновление базовых свойств продукта
        /// </summary>
        /// <param name="product">обновленный продукт</param>
        /// <returns>СтатусКод</returns>

        [HttpPatch(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update( [FromBody] Product product)
        {
            bool result = await productsService.Update(product);
            return result ? Ok() : NotFound();
        }
    }
}
