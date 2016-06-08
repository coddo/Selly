using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LoggingService;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

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
                var response = await ProductCore.CreateAsync(product).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<ProductController>(e);

                return Ok(ResponseFactory<Product>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var response = await ProductCore.GetAllAsync(new[]
                {
                    nameof(Product.ValueAddedTax)
                }).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<ProductController>(e);

                return Ok(ResponseFactory<IList<Product>>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}