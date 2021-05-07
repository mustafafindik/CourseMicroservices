using Course.Services.Catalog.Repositories.Abstract;
using Course.Services.Catalog.Services.Abstract;
using Course.Services.Catalog.Utilities.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Repositories.Concrete
{
    public class CourseRepository: ICourseRepository
    {
        private readonly IMongoCollection<Entities.Concrete.Course> _courseCollection;

        public CourseRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Entities.Concrete.Course>(databaseSettings.CourseCollectionName);
        }


        public async Task<IEnumerable<Entities.Concrete.Course>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(p => true).ToListAsync();
            return courses;
        }

        public async Task<Entities.Concrete.Course> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            return course;
        }

        public async Task<Entities.Concrete.Course> GetByNameAsync(string courseName)
        {
            var course = await _courseCollection.Find(p => p.CourseName.ToLower().Trim() == courseName.ToLower().Trim()).FirstOrDefaultAsync();
            return course;
        }

        public async Task<IEnumerable<Entities.Concrete.Course>> GetByCategoryNameAsync(string categoryName)
        {
            FilterDefinition<Entities.Concrete.Course> filter = Builders<Entities.Concrete.Course>.Filter.Eq(p => p.Category.CategoryName, categoryName);

            return await _courseCollection.Find(filter).ToListAsync();
        }

        public async Task CreateCourse(Entities.Concrete.Course course)
        {
            await _courseCollection.InsertOneAsync(course);
        }


        public async Task<bool> UpdateCourse(Entities.Concrete.Course course)
        {
            var updateResult = await _courseCollection.ReplaceOneAsync(filter: g => g.Id == course.Id, replacement: course);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCourse(string id)
        {
            FilterDefinition<Entities.Concrete.Course> filter = Builders<Entities.Concrete.Course>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _courseCollection.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}
