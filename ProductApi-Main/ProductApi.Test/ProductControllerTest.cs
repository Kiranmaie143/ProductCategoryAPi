using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductApi.Test
{
    public class ProductControllerTest
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productServiceMock = new Mock<IProductService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ProductControllerTest()
        {
            _productController = new ProductController(_productServiceMock.Object,_mapperMock.Object);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnAProduct_WhenProductExist()
        {
           var categoryId= ObjectId.GenerateNewId().ToString();
            var name="sorybook";
            var capacity = "A";
            var price = 10;
           

            var category= new Category
            {
                Id = categoryId,
                Name = name,
                Capacity = capacity,
                Price = price
            };

            _productServiceMock.Setup(x => x.GetAllAsyncById(categoryId)).ReturnsAsync(() => null);

            var productResult = await _productController.GetProductCategoryById(categoryId);

            Assert.IsType<Category>(productResult);
            

        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenReceivedProductNotExist()
        {
            var routeId = ObjectId.GenerateNewId().ToString();

            _productServiceMock.Setup(x => x.GetByIdAsync(routeId))
                .ReturnsAsync(() => null);

            var productResult = await _productController.GetProductById(routeId);

            Assert.IsType<NotFoundResult>(productResult.Result);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenRouteIdIsDifferentToProductId()
        {
            var routeId = ObjectId.GenerateNewId().ToString();

            var productId = ObjectId.GenerateNewId().ToString();
            var productName = "ABC";
            var category = "srd";
            var price = 10;
            
            var product = new Product
            {
                Id = productId,
                Name = productName,
                Category = category,
                Price = price
            };

            var productResult = await _productController.UpdateProduct(routeId, product);

            Assert.IsType<NotFoundResult>(productResult);

        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenProductNotExist()
        {
            var routeId = ObjectId.GenerateNewId().ToString();

            var productId = ObjectId.GenerateNewId().ToString();
            var productName = "ABC";
            var category = "srd";
            var price = 10;

            var product = new Product
            {
                Id = productId,
                Name = productName,
                Category = category,
                Price = price
            };

            _productServiceMock.Setup(x => x.GetByIdAsync(routeId)).ReturnsAsync(() => null);

            var productResult = await _productController.UpdateProduct(routeId, product);

            Assert.IsType<NotFoundResult>(productResult);

        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNoContent_WhenProductExistAndIsUpdated()
        {
            var routeId = ObjectId.GenerateNewId().ToString();

            var productId = ObjectId.GenerateNewId().ToString();
            var productName = "ABC";
            var category = "srd";
            var price = 10;

            var product = new Product
            {
                Id = productId,
                Name = productName,
                Category = category,
                Price = price
            };

            _productServiceMock.Setup(x => x.GetByIdAsync(routeId)).ReturnsAsync(new Product { Id = routeId});
            _productServiceMock.Setup(x => x.UpdateAsync(routeId, product));

            var productResult = await _productController.UpdateProduct(routeId, product);

            Assert.IsType<NoContentResult>(productResult);

        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnBadRequest_WhenProductNotExist()
        {

            var routeId = ObjectId.GenerateNewId().ToString();

            _productServiceMock.Setup(x => x.GetByIdAsync(routeId)).ReturnsAsync(() => null);

            var productResult = await _productController.DeleteProduct(routeId);

            Assert.IsType<BadRequestResult>(productResult);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenProductExist()
        {
            var routeId = ObjectId.GenerateNewId().ToString();

            _productServiceMock.Setup(x => x.GetByIdAsync(routeId)).ReturnsAsync(new Product { Id = routeId});

            _productServiceMock.Setup(x => x.DeleteAsync(routeId));

            var productResult = await _productController.DeleteProduct(routeId);

            Assert.IsType<NoContentResult>(productResult);


        }


    }
}
