using System;
using System.Collections.Generic;
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
                var result = await OrderCore.GetAllAsync(orderBy, orderAscending, new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems)
                }).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Order>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("GetAllForUser")]
        public async Task<IHttpActionResult> GetAllForUser(Guid userId, string orderBy = "", bool orderAscending = true)
        {
            try
            {
                var result = await OrderCore.GetAllForUserAsync(userId, orderBy, orderAscending, new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems)
                }).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Order>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Order order)
        {
            try
            {
                var result = await OrderCore.CreateAsync(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception ex)
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
                var result = await OrderCore.UpdateAsync(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}