using Course.Services.Order.Domain.Repositories;
using Course.Services.Order.Infrastructure.Data;
using Course.Services.Order.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Domain.OrderAggregate.Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.OrderAggregate.Order>> GetOrdersByUserId(string userId)
        {
            var orderList = await _dbContext.Orders.Include(x => x.OrderItems)
                               .Where(o => o.BuyerId == userId)
                               .ToListAsync();
            return orderList;
        }
    }
}