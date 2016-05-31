using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.ClientServerInteraction;

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
                if (order.ClientId == Guid.Empty || order.OrderItems == null || order.OrderItems.Count == 0)
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
                if (order.ClientId == Guid.Empty || order.OrderItems == null || order.OrderItems.Count == 0 ||
                    order.OrderItems.Any(orderItem => orderItem.Id == Guid.Empty))
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

        #endregion
    }
}