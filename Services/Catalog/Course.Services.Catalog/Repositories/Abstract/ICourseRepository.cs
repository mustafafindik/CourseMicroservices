using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Repositories.Abstract
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Entities.Concrete.Course>> GetAllAsync();
        Task<Entities.Concrete.Course> GetByIdAsync(string id);
        Task<Entities.Concrete.Course> GetByNameAsync(string courseName);
        Task<IEnumerable<Entities.Concrete.Course>> GetCourseByCategoryId(string categoryId);
        Task CreateCourse(Entities.Concrete.Course course);
        Task<bool> UpdateCourse(Entities.Concrete.Course course);
        Task<bool> DeleteCourse(string id);
    }
}
