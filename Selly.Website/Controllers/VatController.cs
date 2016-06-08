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
            catch (Exception e)
            {
                LogHelper.LogException<VatController>(e);

                return Ok(ResponseFactory<IList<ValueAddedTax>>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}