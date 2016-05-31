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
        public async Task<IHttpActionResult> GetAll()
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

                return Ok(ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.OK, orders));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("GetAllForUser")]
        public async Task<IHttpActionResult> GetAllForUser(Guid userId)
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
    }
}