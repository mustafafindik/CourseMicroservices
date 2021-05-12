using Course.Services.Order.Application.Dtos;
using Course.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<IDataResult<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
