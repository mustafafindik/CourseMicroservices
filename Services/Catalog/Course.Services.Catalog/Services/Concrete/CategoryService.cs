using AutoMapper;
using Course.Services.Catalog.Dtos.Concrete;
using Course.Services.Catalog.Entities.Concrete;
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
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<CategoryDto>>> GetAllAsync()
        {
            var query = await _categoryRepository.GetAllAsync();
            var queryMap = _mapper.Map<List<CategoryDto>>(query);
            return new SuccessDataResult<List<CategoryDto>>(queryMap, Messages.CategoriesGet);
        }

        public async Task<IDataResult<CategoryDto>> GetByIdAsync(string id)
        {
            var query = await _categoryRepository.GetByIdAsync(id);
            var queryMap = _mapper.Map<CategoryDto>(query);
            return new SuccessDataResult<CategoryDto>(queryMap, Messages.CategoryGet);
        }

        public async Task<IDataResult<CategoryDto>> GetByNameAsync(string categoryName)
        {
            var query = await _categoryRepository.GetByNameAsync(categoryName);
            var queryMap = _mapper.Map<CategoryDto>(query);
            return new SuccessDataResult<CategoryDto>(queryMap, Messages.CategoryGet);
        }

        public async Task<IResult> AddCategory(Category category)
        {
            await _categoryRepository.CreateCategory(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public async Task<IResult> UpdateCategory(Category category)
        {
            var query= await _categoryRepository.UpdateCategory(category);
            if (query)
            {
                return new SuccessResult(Messages.CategoryUpdated);

            }
            return new ErrorResult("Kategori Güncellenemedi");

        }

        public async Task<IResult> DeleteCategory(string id)
        {
            var query =  await _categoryRepository.DeleteCategory(id);
            if (query)
            {
                return new SuccessResult(Messages.CategoryDeleted);
            }
            return new ErrorResult("Kategori Silinemedi");
        }
    }
            
    
}
