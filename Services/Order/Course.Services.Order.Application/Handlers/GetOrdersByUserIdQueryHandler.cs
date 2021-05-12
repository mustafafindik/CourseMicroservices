using AutoMapper;
using Course.Services.Order.Application.Dtos;
using Course.Services.Order.Application.Queries;
using Course.Services.Order.Domain.Repositories;
using Course.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, IDataResult<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper;
        }


        public async Task<IDataResult<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserId(request.UserId);

            if (!orders.Any())
            {
                return new SuccessDataResult<List<OrderDto>>(new List<OrderDto>());
            }

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return new SuccessDataResult<List<OrderDto>>(ordersDto, "Sepet Alınndı");
        }
    }
}
