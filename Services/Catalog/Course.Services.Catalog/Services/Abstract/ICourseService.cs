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
        Task<IResult> AddCourse(Entities.Concrete.Course course);
        Task<IResult> UpdateCourse(Entities.Concrete.Course course);
        Task<IResult> DeleteCourse(string id);
    }
}
