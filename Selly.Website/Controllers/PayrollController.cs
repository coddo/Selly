using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LoggingService;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;
using Selly.Website.Models;

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
                var response = await PayrollCore.GetAllAsync().ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetForOrder")]
        public async Task<IHttpActionResult> GetForOrder(Guid orderId)
        {
            try
            {
                var response = await PayrollCore.GetForOrder(orderId).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<PayrollController>(e);

                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetAllForClient")]
        public async Task<IHttpActionResult> GetAllForClient(Guid clientId)
        {
            try
            {
                var response = await PayrollCore.GetAllForClient(clientId).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Payroll>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> MakePayment([FromBody] MakePaymentModel model)
        {
            try
            {
                var response = await PayrollCore.MakePayment(model.OrderId, model.ClientId).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<PayrollController>(e);

                return Ok(ResponseFactory.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}