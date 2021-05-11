using Course.Services.Basket.Dtos;
using Course.Services.Basket.Repository;
using Course.Services.Basket.Services.Abstract;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly RedisRepository _redisRepository;

        public BasketService(RedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }


        public async Task<IResult> Delete(string userId)
        {
            var status = await _redisRepository.GetDb().KeyDeleteAsync(userId);
            return status ? new SuccessResult("Sepet Başarıyla Silindi") : new ErrorResult("Sepet Silinirken Hata Oluştu");
        }

        public async Task<IDataResult<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisRepository.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return new ErrorDataResult<BasketDto>("Sepet Alınırken Hata oluştu");
            }

            return  new SuccessDataResult<BasketDto>(JsonSerializer.Deserialize<BasketDto>(existBasket), "Sepet Başarıyla Alındı");
        }

        public async Task<IResult> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisRepository.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? new SuccessResult("Sepet Başarıyla Eklendi") : new ErrorResult("Sepet Eklenirken Hata Oluştu");
        }
    }
}
