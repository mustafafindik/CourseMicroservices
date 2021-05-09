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
        Task<IDataResult<List<CourseDto>>> GetCourseByCategoryId(string categoryId);
        Task<IResult> AddCourse(CourseCrudDto CourseCrudDto);
        Task<IResult> UpdateCourse(CourseCrudDto CourseCrudDto);
        Task<IResult> DeleteCourse(string id);
    }
}
