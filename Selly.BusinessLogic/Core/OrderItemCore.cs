using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class OrderItemCore : BaseCore<OrderItemRepository, Models.OrderItem, DataLayer.OrderItem>
    {
        private OrderItemCore()
        {
        }

        public static OrderItemCore Instance => new OrderItemCore();
    }
}