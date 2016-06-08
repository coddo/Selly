using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

namespace Selly.Website.Controllers
{
    public class VatController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var response = await VatCore.GetAllAsync().ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<ValueAddedTax>>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}