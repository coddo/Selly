using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.BusinessLogic.Utility;
using Selly.Models;
using Selly.Models.Common.ClientServerInteraction;
using Selly.Models.Enums;

namespace Selly.Website.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll(string orderBy = "", bool orderAscending = true)
        {
            try
            {
                var orders = await OrderCore.GetAllAsync(new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems)
                }).ConfigureAwait(false);

                if (orders == null || orders.Count == 0)
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.NoContent));
                }

                orders = SortOrders(orders, orderBy, orderAscending);

                return Ok(ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.OK, orders));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("GetAllForUser")]
        public async Task<IHttpActionResult> GetAllForUser(Guid userId, string orderBy = "", bool orderAscending = true)
        {
            try
            {
                var orders = await OrderCore.GetAllForUserAsync(userId, new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems)
                }).ConfigureAwait(false);

                if (orders == null || orders.Count == 0)
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.NoContent));
                }

                orders = SortOrders(orders, orderBy, orderAscending);

                return Ok(ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.OK, orders));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Order order)
        {
            try
            {
                if (!ValidateOrder(order))
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                foreach (var orderItem in order.OrderItems.Where(orderItem => orderItem.Id == Guid.Empty))
                {
                    orderItem.Id = Guid.NewGuid();
                }

                var result = await OrderCore.CreateAsync(order).ConfigureAwait(false);
                if (result == null)
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                return Ok(ResponseFactory<Order>.CreateResponse(true, HttpStatusCode.OK, result));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IHttpActionResult> Update([FromBody] Order order)
        {
            try
            {
                if (!ValidateOrder(order, false))
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                var result = await OrderCore.UpdateAsync(order).ConfigureAwait(false);

                if (result == null)
                {
                    return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                return Ok(ResponseFactory<Order>.CreateResponse(true, HttpStatusCode.OK, result));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        #region Private methods

        private IList<Order> SortOrders(IList<Order> orders, string orderBy, bool orderAscending)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return orders;
            }

            Func<Order, object> orderClause;
            switch (orderBy)
            {
                case nameof(Order.ClientId):
                    orderClause = order => order.ClientId;
                    break;
                case nameof(Order.CurrencyId):
                    orderClause = order => order.CurrencyId;
                    break;
                case nameof(Order.Date):
                    orderClause = order => order.Date;
                    break;
                case nameof(Order.SaleType):
                    orderClause = order => order.SaleType;
                    break;
                case nameof(Order.Status):
                    orderClause = order => order.Status;
                    break;
                default:
                    return orders;
            }

            return orderAscending ? orders.OrderBy(orderClause).ToArray() : orders.OrderByDescending(orderClause).ToArray();
        }

        private static bool ValidateOrder(Order order, bool tolerateEmptyGuids = true)
        {
            if (order.ClientId == Guid.Empty || order.OrderItems == null || order.OrderItems.Count == 0)
            {
                return false;
            }

            if (!tolerateEmptyGuids)
            {
                return order.OrderItems.All(orderItem => orderItem.Id != Guid.Empty);
            }

            if (!order.OrderItems.All(orderItem => orderItem.Price > 0))
            {
                return false;
            }

            return ValidateOrderItems(order.OrderItems, (SaleType) order.SaleType);
        }

        private static bool ValidateOrderItems(IEnumerable<OrderItem> orderItems, SaleType orderType)
        {
            switch (orderType)
            {
                case SaleType.Normal:
                    return orderItems.All(orderItem => orderItem.Quantity > 0);
                case SaleType.Return:
                    return orderItems.All(orderItem => orderItem.Quantity < 0);
                case SaleType.Exchange:
                    return orderItems.All(orderItem => !FloatingPointUtility.AreEqual(orderItem.Quantity, 0));
                default:
                    return false;
            }
        }

        #endregion
    }
}