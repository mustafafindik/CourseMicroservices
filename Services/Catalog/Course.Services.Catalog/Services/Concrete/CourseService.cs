using AutoMapper;
using Course.Services.Catalog.Dtos.Concrete;
using Course.Services.Catalog.Repositories.Abstract;
using Course.Services.Catalog.Services.Abstract;
using Course.Services.Catalog.Utilities.Constants;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services.Concrete
{
    public class CourseService: ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<CourseDto>>> GetAllAsync()
        {
            var query = await _courseRepository.GetAllAsync();
            var queryMap = _mapper.Map<List<CourseDto>>(query);
            return new SuccessDataResult<List<CourseDto>>(queryMap, Messages.CoursesGet);
        }

        public async Task<IDataResult<CourseDto>> GetByIdAsync(string id)
        {
            var query = await _courseRepository.GetByIdAsync(id);
            var queryMap = _mapper.Map<CourseDto>(query);
            return new SuccessDataResult<CourseDto>(queryMap, Messages.CourseGet);
        }

        public async Task<IDataResult<CourseDto>> GetByNameAsync(string courseName)
        {
            var query = await _courseRepository.GetByNameAsync(courseName);
            var queryMap = _mapper.Map<CourseDto>(query);
            return new SuccessDataResult<CourseDto>(queryMap, Messages.CourseGet);
        }

        public async Task<IDataResult<CourseDto>> GetByCategoryNameAsync(string categoryName)
        {
            var query = await _courseRepository.GetByCategoryNameAsync(categoryName);
            var queryMap = _mapper.Map<CourseDto>(query);
            return new SuccessDataResult<CourseDto>(queryMap, Messages.CourseGet);
        }

        public async Task<IResult> AddCourse(Entities.Concrete.Course course)
        {
            await _courseRepository.CreateCourse(course);
            return new SuccessResult(Messages.CourseAdded);
        }

        public async Task<IResult> UpdateCourse(Entities.Concrete.Course course)
        {
            await _courseRepository.UpdateCourse(course);
            return new SuccessResult(Messages.CourseUpdated);
        }

        public async Task<IResult> DeleteCourse(string id)
        {
            await _courseRepository.DeleteCourse(id);
            return new SuccessResult(Messages.CourseDeleted);
        }

    }
}
