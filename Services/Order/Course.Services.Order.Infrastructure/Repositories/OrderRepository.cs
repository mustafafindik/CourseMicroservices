using Course.Services.Order.Domain.Repositories;
using Course.Services.Order.Infrastructure.Data;
using Course.Services.Order.Infrastructure.Repositories.Base;
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

       
    }
}