using Gedonist.Controllers;
using Gedonist.Core.BindingModels;
using Gedonist.Core.Interfaces;
using Gedonist.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;

namespace Gedonist.Tests
{
    public class CategoryControllerTests
    {
        [Fact]
        public async Task GetAll_Collection_OkCollection()
        {
            //Arrange
            List<Category> categories = new List<Category>();
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAll()).Returns(Task.FromResult<List<Category>?>(categories));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllWithData();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_null_NotFound()
        {
            //Arrange
            List<Category>? categories = null;
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAll()).Returns(Task.FromResult<List<Category>?>(categories));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllWithData();
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllNames_Collection_OkCollection()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAllNames()).Returns(Task.FromResult<List<string>?>( new List<string> { "g"}));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllNames();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllNames_null_NotFound()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAllNames()).Returns(Task.FromResult<List<string>?>(new List<string> { "g" }));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllWithData();
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllWithRelationships_Collection_OkCollection()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAllWithRelationships()).Returns(Task.FromResult<List<Category>?>(new List<Category>()));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllWithRelationships();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllWithRelationships_null_NotFound()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetAllWithRelationships()).Returns(Task.FromResult<List<Category>?>(null));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetAllWithData();
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_int_Category()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetById(1)).Returns(Task.FromResult<Category?>(new Category() { Name = "P"}));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_noncorrectint_null()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.GetById(-1)).Returns(Task.FromResult<Category?>(null));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.GetById(-1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_bindingmodel_Ok()
        {
            //Arrange
            BindingCategory category = new BindingCategory();
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Create(category)).Returns(Task.FromResult(true));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Create(category);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_null_Conflict()
        {
            //Arrange
            BindingCategory category = new BindingCategory();
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Create(category)).Returns(Task.FromResult(false));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Create(category);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Update_Category_Ok()
        {
            //Arrange
            BindingCategory bindingCategory = new BindingCategory(){ Name = "P" };
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Update(bindingCategory)).Returns(Task.FromResult(true));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Update(bindingCategory);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_noncorrectCategory_Confict()
        {
            //Arrange
            BindingCategory bindingCategory = new BindingCategory() { Name = "P" };
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Update(bindingCategory)).Returns(Task.FromResult(false));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Update(bindingCategory);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Delete_int_Ok()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Delete(1)).Returns(Task.FromResult(true));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Delete(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_noncorrectint_NotFound()
        {
            //Arrange
            var mock = new Mock<ICategoriesService>();
            mock.Setup(x => x.Delete(-1)).Returns(Task.FromResult(false));
            CategoryController controller = new CategoryController(mock.Object);
            //Action
            var result = await controller.Delete(-1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
