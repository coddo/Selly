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
    public class PayrollController : ApiController
    {
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var payrolls = await PayrollCore.GetAllAsync().ConfigureAwait(false);
                if (payrolls == null || payrolls.Count == 0)
                {
                    return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(true, HttpStatusCode.OK, payrolls));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("GetForOrder")]
        public async Task<IHttpActionResult> GetForOrder(Guid orderId)
        {
            try
            {
                var payroll = await PayrollCore.GetForOrder(orderId).ConfigureAwait(false);
                if (payroll == null)
                {
                    return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<Payroll>.CreateResponse(true, HttpStatusCode.OK, payroll));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [ActionName("GetAllForClient")]
        public async Task<IHttpActionResult> GetAllForClient(Guid clientId)
        {
            try
            {
                var payrolls = await PayrollCore.GetAllForClient(clientId).ConfigureAwait(false);
                if (payrolls == null || payrolls.Count == 0)
                {
                    return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(true, HttpStatusCode.NoContent));
                }

                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(true, HttpStatusCode.OK, payrolls));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}