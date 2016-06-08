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
    public class CurrencyController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var response = await CurrencyCore.GetAllAsync().ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<CurrencyController>(e);

                return Ok(ResponseFactory<IList<Currency>>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}