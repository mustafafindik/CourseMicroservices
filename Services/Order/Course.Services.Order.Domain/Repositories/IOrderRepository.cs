using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Domain.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Domain.OrderAggregate.Order>
    {
    }
}
