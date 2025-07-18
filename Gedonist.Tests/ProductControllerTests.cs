using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gedonist.Core.Models;
using Gedonist.Core.Interfaces;
using Moq;
using Gedonist.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Gedonist.Core.BindingModels;

namespace Gedonist.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetAll_Collection_OKCollection()
        {
            //Arrange
            List<Product> products = new List<Product>();
            var mock = new Mock<IProductsService>();
            mock.Setup(m => m.GetAll()).ReturnsAsync(products);
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_Null_NotFound()
        {
            //Arrange
            var mock = new Mock<IProductsService>();
            mock.Setup(m => m.GetAll()).Returns(Task.FromResult<List<Product>?>(null));
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.GetAll();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Delete_int_OK()
        {
            //Arrange
            var mock = new Mock<IProductsService>();
            mock.Setup(m => m.Delete(1)).ReturnsAsync(true);
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.Delete(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_noncorrectint_NotFound()
        {
            //Arrange
            var mock = new Mock<IProductsService>();
            mock.Setup(m => m.Delete(-1)).ReturnsAsync(false);
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.Delete(-1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_newmodel_Created()
        {
            //Arrange
            BindingProduct product = new BindingProduct();
            var mock = new Mock<IProductsService>();
            mock.Setup(x => x.Create(product)).ReturnsAsync(true);
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.Create(product);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_oldmodel_NotFound()
        {
            //Arrange
            BindingProduct product = new BindingProduct();
            var mock = new Mock<IProductsService>();
            mock.Setup(x => x.Create(product)).ReturnsAsync(false);
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.Create(product);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_int_Okmodel()
        {
            //Arrange
            var mock = new Mock<IProductsService>();
            mock.Setup(x => x.GetById(1)).ReturnsAsync(new Product { Name = "PP" });
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.GetById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_noncorrectint_NotFound()
        {
            //Arrange
            var mock = new Mock<IProductsService>();
            mock.Setup(x => x.GetById(-2)).Returns(Task.FromResult<Product?>(null));
            ProductController controller = new ProductController(mock.Object);
            //Action
            var result = await controller.GetById(-2);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }



    }
}
