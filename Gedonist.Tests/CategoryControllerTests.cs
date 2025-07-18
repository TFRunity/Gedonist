using Gedonist.Controllers;
using Gedonist.Core.Interfaces;
using Gedonist.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Text.Json;

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




    }
}
