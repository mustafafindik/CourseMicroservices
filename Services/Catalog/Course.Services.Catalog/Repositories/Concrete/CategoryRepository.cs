using Course.Services.Catalog.Entities.Concrete;
using Course.Services.Catalog.Repositories.Abstract;
using Course.Services.Catalog.Utilities.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Repositories.Concrete
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(p => true).ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            var category = await _categoryCollection.Find(p=>p.CategoryName.ToLower().Trim() == categoryName.ToLower().Trim()).FirstOrDefaultAsync();
            return category;
        }

        public async Task CreateCategory(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
        }


        public async Task<bool> UpdateCategory(Category category)
        {
            var updateResult = await _categoryCollection.ReplaceOneAsync(filter: g => g.Id == category.Id, replacement: category);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _categoryCollection.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}
