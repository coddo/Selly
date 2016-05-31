using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models.Common.ClientServerInteraction;
using Selly.Website.Models;

namespace Selly.Website.Controllers
{
    public class SummaryController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var products = await ProductCore.GetAllAsync().ConfigureAwait(false);
                var orders = await OrderCore.GetAllAsync().ConfigureAwait(false);
                var orderItems = await OrderItemCore.GetAllAsync().ConfigureAwait(false);
                var clients = await ClientCore.GetAllAsync().ConfigureAwait(false);
                var payments = await PayrollCore.GetAllAsync().ConfigureAwait(false);

                var totalIncomeValue = payments.Sum(payment => payment.Value);
                var totalOrdersValue = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

                var model = new SummaryModel
                {
                    NumberOfClients = clients.Count,
                    NumberOfOrders = orders.Count,
                    NumberOfPayments = payments.Count,
                    NumberOfProducts = products.Count,
                    TotalIncomeValue = totalIncomeValue,
                    TotalOrdersValue = totalOrdersValue
                };

                return Ok(ResponseFactory<SummaryModel>.CreateResponse(true, HttpStatusCode.OK, model));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<SummaryModel>.CreateResponse(false, HttpStatusCode.InternalServerError));
            }
        }
    }
}