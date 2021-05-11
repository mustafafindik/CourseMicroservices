using Course.Services.Basket.Dtos;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services.Abstract
{
    public interface IBasketService
    {
       
        Task<IDataResult<BasketDto>> GetBasket(string userId);

        Task<IResult> SaveOrUpdate(BasketDto basketDto);

        Task<IResult> Delete(string userId);
    }
}
