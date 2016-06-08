using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

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
                var response = await OrderItemCore.GetAllAsync().ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("Update")]
        public async Task<IHttpActionResult> Update([FromBody] OrderItem orderItem)
        {
            try
            {
                var response = await OrderItemCore.UpdateAsync(orderItem).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<OrderItem>>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}