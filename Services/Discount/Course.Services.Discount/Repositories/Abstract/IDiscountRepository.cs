using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Repositories.Abstract
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Entities.Discount>> GetAll();
        Task<Entities.Discount> GetById(int id);
        Task<bool> Save(Entities.Discount discount);
        Task<bool> Update(Entities.Discount discount);
        Task<bool> Delete(int id);
        Task<Entities.Discount> GetByCodeAndUserId(string code, string userId);
    }
}
