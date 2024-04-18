using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProductApi.Dtos;
using ProductApi.Models;
using ProductApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetProductcategories")]
        public async Task<IEnumerable<Category>> GetProductCategoryById(string id)
        {
            var categories = await _productService.GetAllAsyncById(id);

             return categories;
        } 

        [HttpPost]
        public async Task<ActionResult<Category>>CreateProduct(Category createProduct)
        {
            var category = _mapper.Map<Category>(createProduct);

            await _productService.CreateAsync(category);

            return CreatedAtRoute("GetProduct", new { id = category.Id, category });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(string id, Category category)
        {
            if (id != category.Id) return NotFound();

            var existingProduct = await _productService.GetAllAsyncById(id);

            if (existingProduct == null) return NotFound();

            await _productService.UpdateAsync(id, category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var product = await _productService.GetAllAsyncById(id);

            if (product == null) return BadRequest();

            await _productService.DeleteAsync(id);

            return NoContent();
        }

    }
}
