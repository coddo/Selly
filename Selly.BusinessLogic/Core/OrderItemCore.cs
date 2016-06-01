using System.Net;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.BusinessLogic.Validation;
using Selly.DataAdapter;
using Selly.DataLayer;
using Selly.DataLayer.Repositories;
using Selly.Models.Common.ClientServerInteraction;
using Selly.Models.Enums;
using OrderItem = Selly.Models.OrderItem;

namespace Selly.BusinessLogic.Core
{
    public class OrderItemCore : BaseSinglePkCore<OrderItemRepository, OrderItem, DataLayer.OrderItem>
    {
        private OrderItemCore()
        {
        }

        public new static async Task<Response<OrderItem>> UpdateAsync(OrderItem orderItem)
        {
            using (var unitOfWork = new DataLayerUnitOfWork())
            {
                var order = await unitOfWork.TrackingRepository<OrderRepository>().GetAsync(new[] { orderItem.OrderId}).ConfigureAwait(false);

                if (!SalesValidator.ValidateOrderItem(orderItem, (SaleType) order.SaleType))
                {
                    return ResponseFactory<OrderItem>.CreateResponse(false, HttpStatusCode.BadRequest);
                }

                var dbModel = orderItem.CopyTo<DataLayer.OrderItem>();
                var result = await unitOfWork.TrackingRepository<OrderItemRepository>().UpdateAsync(dbModel).ConfigureAwait(false);

                if (result == null)
                {
                    return ResponseFactory<OrderItem>.CreateResponse(false, HttpStatusCode.BadRequest);
                }

                return ResponseFactory<OrderItem>.CreateResponse(true, HttpStatusCode.OK, result.CopyTo<OrderItem>());
            }
        }
    }
}