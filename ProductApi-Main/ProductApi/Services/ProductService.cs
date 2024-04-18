using MongoDB.Driver;
using ProductApi.Data;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Category> _categories;

        public ProductService(IProductDataSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.Database);
            _products = database.GetCollection<Product>(settings.Collection);
            _categories = database.GetCollection<Category>(settings.Collection);
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
            return category;
        }

        public async Task DeleteAsync(string id)
        {
            await _categories.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.FindAsync(p => true).Result.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsyncById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, id);
            return await _products.Find(filter).Project(p => p.Categories).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Category category)
        {
            await _categories.ReplaceOneAsync(p => p.Id == id, category); 
        }
    }
}
