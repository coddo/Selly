using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.DataAdapter;
using Selly.DataLayer;
using Selly.DataLayer.Repositories;
using Order = Selly.Models.Order;

namespace Selly.BusinessLogic.Core
{
    public class OrderCore : BaseCore<OrderRepository, Models.Order, DataLayer.Order>
    {
        private OrderCore()
        {
        }

        public static async Task<IList<Order>> GetAllForUserAsync(Guid clientId, IList<string> navigationProperties = null)
        {
            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var orders = await orderRepository.GetListAsync(order => order.ClientId == clientId, navigationProperties);

                return orders.CopyTo<Order>();
            }
        }
    }
}