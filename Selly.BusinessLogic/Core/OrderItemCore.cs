using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class OrderItemCore : BaseCore<OrderItemRepository, Models.OrderItem, DataLayer.OrderItem>
    {
        private static OrderItemCore mInstance;

        public static OrderItemCore Instance => mInstance ?? (mInstance = new OrderItemCore());
    }
}