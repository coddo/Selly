using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.BusinessLogic.Validation;
using Selly.DataAdapter;
using Selly.DataLayer.Extensions;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;
using Selly.Models.Common.Response;
using Selly.Models.Enums;

namespace Selly.BusinessLogic.Core
{
    public class OrderItemCore : BaseSinglePkCore<OrderItemRepository, OrderItem, DataLayer.OrderItem>
    {
        private OrderItemCore()
        {
        }

        public static async Task<Response<OrderItem>> UpdateAsync(OrderItem orderItem)
        {
            using (var unitOfWork = new DataLayerUnitOfWork())
            {
                var order = await unitOfWork.TrackingRepository<OrderRepository>().GetAsync(orderItem.OrderId).ConfigureAwait(false);

                if (!SalesValidator.ValidateOrderItem(orderItem, (SaleType) order.SaleType))
                {
                    return ResponseFactory<OrderItem>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                var dbModel = orderItem.CopyTo<DataLayer.OrderItem>();
                var result = await unitOfWork.TrackingRepository<OrderItemRepository>().UpdateAsync(dbModel).ConfigureAwait(false);

                if (result == null)
                {
                    return ResponseFactory<OrderItem>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<OrderItem>.CreateResponse(true, ResponseCode.Success, result.CopyTo<OrderItem>());
            }
        }
    }
}