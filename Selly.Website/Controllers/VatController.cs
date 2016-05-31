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
    public class VatController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var valueAddedTaxes = await VatCore.GetAllAsync().ConfigureAwait(false);
                if (valueAddedTaxes == null || valueAddedTaxes.Count == 0)
                {
                    return Ok(ResponseFactory<IList<ValueAddedTax>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<ValueAddedTax>>.CreateResponse(true, HttpStatusCode.OK, valueAddedTaxes));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<ValueAddedTax>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}