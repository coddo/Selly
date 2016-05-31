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
    public class OrderItemController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var orderItems = await OrderItemCore.GetAllAsync().ConfigureAwait(false);
                if (orderItems == null || orderItems.Count == 0)
                {
                    return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(true, HttpStatusCode.OK, orderItems));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("Update")]
        public async Task<IHttpActionResult> Update([FromBody] OrderItem orderItem)
        {
            try
            {
                var result = await OrderItemCore.UpdateAsync(orderItem).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}