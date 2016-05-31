using System;
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
        //[HttpGet]
        //[ActionName("GetAll")]
        //public async Task<IHttpActionResult> 

        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Order order)
        {
            try
            {
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
