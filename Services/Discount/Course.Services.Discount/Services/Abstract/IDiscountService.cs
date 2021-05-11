using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services.Abstract
{
    public interface IDiscountService
    {
        Task<IDataResult<List<Entities.Discount>>> GetAllAsync();
        Task<IDataResult<Entities.Discount>> GetByIdAsync(int id);
        Task<IResult> Save(Entities.Discount discount);
        Task<IResult> Update(Entities.Discount discount);
        Task<IResult> Delete(int id);
        Task<IDataResult<Entities.Discount>> GetByCodeAndUserIdAsync(string code, string userId);
    }
}
