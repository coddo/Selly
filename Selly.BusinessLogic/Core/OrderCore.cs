using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class OrderCore : BaseCore<OrderRepository, Models.Order, DataLayer.Order>
    {
        private OrderCore()
        {
        }
    }
}