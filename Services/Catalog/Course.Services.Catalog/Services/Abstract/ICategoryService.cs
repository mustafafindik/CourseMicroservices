using Course.Services.Catalog.Dtos.Concrete;
using Course.Services.Catalog.Entities.Concrete;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<CategoryDto>>> GetAllAsync();
        Task<IDataResult<CategoryDto>> GetByIdAsync(string id);
        Task<IDataResult<CategoryDto>> GetByNameAsync(string categoryName);
        Task<IResult> AddCategory(Category category);
        Task<IResult> UpdateCategory(Category category);
        Task<IResult> DeleteCategory(string id);
    }
}
