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

        public async Task<IDataResult<List<CourseDto>>> GetCourseByCategoryId(string categoryId)
        {
            var query = await _courseRepository.GetCourseByCategoryId(categoryId);
            var queryMap = _mapper.Map<List<CourseDto>>(query);
            return new SuccessDataResult<List<CourseDto>>(queryMap, Messages.CourseGet);
        }

        public async Task<IResult> AddCourse(CourseCrudDto courseCrudDto)
        {
            var course = _mapper.Map<Entities.Concrete.Course>(courseCrudDto);
            await _courseRepository.CreateCourse(course);
            return new SuccessResult(Messages.CourseAdded);
        }

        public async Task<IResult> UpdateCourse(CourseCrudDto courseCrudDto)
        {
            var course = _mapper.Map<Entities.Concrete.Course>(courseCrudDto);
            var query = await _courseRepository.UpdateCourse(course);
            if (query)
            {
                return new SuccessResult(Messages.CourseUpdated);
            }
            return new ErrorResult("Kurs Güncellenemedi");

        }

        public async Task<IResult> DeleteCourse(string id)
        {
            var query = await _courseRepository.DeleteCourse(id);
            if (query)
            {
                return new SuccessResult(Messages.CourseDeleted);
            }
            return new ErrorResult("Kurs Silinemedi");

        }

    }
}
