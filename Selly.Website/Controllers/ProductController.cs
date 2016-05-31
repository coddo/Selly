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
    public class ProductController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var products = await ProductCore.GetAllAsync().ConfigureAwait(false);
                if (products == null || products.Count == 0)
                {
                    return Ok(ResponseFactory<IList<Product>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<Product>>.CreateResponse(true, HttpStatusCode.OK, products));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Product>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}