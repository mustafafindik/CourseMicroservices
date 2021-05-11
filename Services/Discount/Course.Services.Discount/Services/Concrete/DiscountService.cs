using Course.Services.Discount.Repositories.Abstract;
using Course.Services.Discount.Services.Abstract;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;

        }
        public async Task<IResult> Delete(int id)
        {
            var query = await _discountRepository.Delete(id);
            if (query)
            {
                return new SuccessResult("İndirim Silindi");
            }
            return new ErrorResult("İndirim Silinemedi");
        }

        public async Task<IDataResult<List<Entities.Discount>>> GetAllAsync()
        {
            var query = await _discountRepository.GetAll();
            return new SuccessDataResult<List<Entities.Discount>>(query.ToList(), "İndirimler Alınd");
        }

        public async Task<IDataResult<Entities.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var query = await _discountRepository.GetByCodeAndUserId(code,userId);
            return new SuccessDataResult<Entities.Discount>(query, "İndirim Alındı");
        }

        public async Task<IDataResult<Entities.Discount>> GetByIdAsync(int id)
        {
            var query = await _discountRepository.GetById(id);
            return new SuccessDataResult<Entities.Discount>(query, "İndirim Alındı");
        }

        public async Task<IResult> Save(Entities.Discount discount)
        {
            var query = await _discountRepository.Save(discount);
            if (query)
            {
                return new SuccessResult("İndirim Kaydedildi");
            }
            return new ErrorResult("İndirim Kaydedilemedi");
        }

        public  async Task<IResult> Update(Entities.Discount discount)
        {
            var query = await _discountRepository.Update(discount);
            if (query)
            {
                return new SuccessResult("İndirim Güncellendi");
            }
            return new ErrorResult("İndirim Güncellemedi");
        }
    }
}
