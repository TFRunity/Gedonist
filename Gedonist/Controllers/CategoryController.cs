using Gedonist.Core.BindingModels;
using Gedonist.Core.Models;
using Gedonist.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gedonist.Controllers
{
    
    /// <summary>
    /// Все операции с базовыми категориями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoriesService categoriesService) : ControllerBase
    {
        private readonly ICategoriesService categoriesService = categoriesService;

        /// <summary>
        /// Доступ ко всем категориям
        /// </summary>
        /// <returns>Все свойства категории/NotFound</returns>
        [HttpGet(nameof(GetAllWithData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWithData()
        {
            List<Category>? categories = await categoriesService.GetAll();
            return categories == null ? NotFound() : Ok(categories);
        }
        
        /// <summary>
        /// Все имена категорий
        /// </summary>
        /// <returns>Все имена категорий</returns>

        [HttpGet(nameof(GetAllNames))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNames()
        {
            List<string>? strings = await categoriesService.GetAllNames();
            return strings == null ? NotFound() : Ok(strings);
        }

        /// <summary>
        /// Доступ к категориям со всеми связанными объектами
        /// </summary>
        /// <returns>Категории со всеми объектами</returns>

        [HttpGet(nameof(GetAllWithRelationships))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllWithRelationships()
        {
            List<Category>? categories = await categoriesService.GetAllWithRelationships();
            return categories == null ? NotFound() : Ok(categories);
        }

        /// <summary>
        /// Возвращает категорию по id
        /// </summary>
        /// <param name="id">id категории</param>
        /// <returns>Категория</returns>

        [HttpGet(nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromBody]int id)
        {
            Category? category = await categoriesService.GetById(id);
            return category == null ? NotFound() : Ok(category);
        }

        /// <summary>
        /// Возвращает категорию по названию, накатить индексы на названия
        /// </summary>
        /// <param name="name">name категории</param>
        /// <returns>Категория</returns>

        [HttpGet(nameof(GetByName))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName([FromBody]string name)
        {
            Category? category = await categoriesService.GetByName(name);
            return category == null ? NotFound() : Ok(category);
        }

        /// <summary>
        /// Создает категорию с заданными базовыми свойствами
        /// </summary>
        /// <param name="category">экзепляр, получаемый из формы</param>
        /// <returns>СтатусКод</returns>

        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromForm] BindingCategory category)
        {
            bool result = await categoriesService.Create(category);
            return result == false ? Conflict("Уже есть такой") : Created();
        }

        /// <summary>
        /// Обновляет базовые поля категории
        /// </summary>
        /// <param name="bindingCategory">обновленная категория, (базовые поля)</param>
        /// <returns>СтатусКод</returns>

        [HttpPatch(nameof(Update))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] BindingCategory bindingCategory)
        {
            bool result = await categoriesService.Update(new Category { Name = bindingCategory.Name });
            return result == false ? Conflict("Не получилось обновить") : Ok();
        }

        /// <summary>
        /// Удаляет категорию каскадно, обрывая связи
        /// </summary>
        /// <param name="id">id категории, которую нужно удалить</param>
        /// <returns>СтатусКод</returns>

        [HttpDelete(nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            bool result = await categoriesService.Delete(id);
            return result == false ? Conflict("Не получилось удалить") : Ok();
        }

    }
}
