using Course.Services.Catalog.Entities.Concrete;
using Course.Services.Catalog.Repositories.Abstract;
using Course.Services.Catalog.Services.Abstract;
using Course.Services.Catalog.Utilities.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Repositories.Concrete
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IMongoCollection<Entities.Concrete.Course> _courseCollection;
        private readonly IMongoCollection<Entities.Concrete.Category> _categoryCollection;
        public CourseRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Entities.Concrete.Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Entities.Concrete.Category>(databaseSettings.CategoryCollectionName);
        }


        public async Task<IEnumerable<Entities.Concrete.Course>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(p => true).ToListAsync();
            if (courses.Any())
            {
                courses.ForEach(async c => c.Category = await _categoryCollection.Find<Category>(ca => ca.Id == c.CategoryId).FirstOrDefaultAsync());

            }

            #region test
            //var result =  (from course in _courseCollection.AsQueryable()
            //                    join category in _categoryCollection.AsQueryable()
            //                    on course.CategoryId equals category.Id
            //                    select new Entities.Concrete.Course
            //                    {
            //                        Category = category,
            //                        CategoryId = course.CategoryId,
            //                        CourseName = course.CourseName,
            //                        CreateDate = course.CreateDate,
            //                        Description = course.Description,
            //                        Feature = course.Feature,
            //                        Id = course.Id,
            //                        Languages = course.Languages,
            //                        Photo = course.Photo,
            //                        Price = course.Price,
            //                        ShortDescription = course.ShortDescription,
            //                        UserId = course.UserId

            //                    }).ToList();



            //var result2 = _courseCollection.AsQueryable().Join(_categoryCollection.AsQueryable(), course => course.CategoryId, category => category.Id, (course, category) => new Entities.Concrete.Course
            //{
            //    Category = category,
            //    CategoryId = course.CategoryId,
            //    CourseName = course.CourseName,
            //    CreateDate = course.CreateDate,
            //    Description = course.Description,
            //    Feature = course.Feature,
            //    Id = course.Id,
            //    Languages = course.Languages,
            //    Photo = course.Photo,
            //    Price = course.Price,
            //    ShortDescription = course.ShortDescription,
            //    UserId = course.UserId

            //}).ToList();
            #endregion




            return courses;
        }

        public async Task<Entities.Concrete.Course> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (course != null)
            {
                course.Category = await _categoryCollection.Find<Category>(ca => ca.Id == course.CategoryId).FirstOrDefaultAsync();
            }

            return course;
        }

        public async Task<Entities.Concrete.Course> GetByNameAsync(string courseName)
        {
            var course = await _courseCollection.Find(p => p.CourseName == courseName).FirstOrDefaultAsync();
            if (course != null)
            {
                course.Category = await _categoryCollection.Find<Category>(ca => ca.Id == course.CategoryId).FirstOrDefaultAsync();
            }
            return course;
        }

        public async Task<IEnumerable<Entities.Concrete.Course>> GetByCategoryNameAsync(string categoryName)
        {
            FilterDefinition<Entities.Concrete.Course> filter = Builders<Entities.Concrete.Course>.Filter.Eq(p => p.Category.CategoryName, categoryName);
            var courses = await _courseCollection.Find(filter).ToListAsync();

            if (courses.Any())
            {
                courses.ForEach(async c => c.Category = await _categoryCollection.Find<Category>(ca => ca.Id == c.CategoryId).FirstOrDefaultAsync());
            }
            return courses;
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
