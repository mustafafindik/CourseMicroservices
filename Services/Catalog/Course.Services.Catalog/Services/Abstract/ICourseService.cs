using Course.Services.Catalog.Dtos.Concrete;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services.Abstract
{
    public interface ICourseService
    {
        Task<IDataResult<List<CourseDto>>> GetAllAsync();
        Task<IDataResult<CourseDto>> GetByIdAsync(string id);
        Task<IDataResult<CourseDto>> GetByNameAsync(string courseName);
        Task<IDataResult<CourseDto>> GetByCategoryNameAsync(string categoryName);
        Task<IResult> AddCategory(Entities.Concrete.Course course);
        Task<IResult> UpdateCategory(Entities.Concrete.Course course);
        Task<IResult> DeleteCategory(string id);
    }
}
