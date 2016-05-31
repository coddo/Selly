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
    public class CurrencyController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var currencies = await CurrencyCore.GetAllAsync().ConfigureAwait(false);
                if (currencies == null || currencies.Count == 0)
                {
                    return Ok(ResponseFactory<IList<Currency>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<Currency>>.CreateResponse(true, HttpStatusCode.OK, currencies));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Currency>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}