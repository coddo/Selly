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
        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Product product)
        {
            try
            {
                var createdProduct = await ProductCore.CreateAsync(product).ConfigureAwait(false);
                if (createdProduct == null)
                {
                    return Ok(ResponseFactory<Product>.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                return Ok(ResponseFactory<Product>.CreateResponse(true, HttpStatusCode.OK, createdProduct));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Product>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

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