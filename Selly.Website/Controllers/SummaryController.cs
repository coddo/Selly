using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models.Common.Response;
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
                var productsResponse = await ProductCore.GetAllAsync().ConfigureAwait(false);
                var ordersResponse = await OrderCore.GetAllAsync().ConfigureAwait(false);
                var orderItemsResponse = await OrderItemCore.GetAllAsync().ConfigureAwait(false);
                var clientsResponse = await ClientCore.GetAllAsync().ConfigureAwait(false);
                var paymentsResponse = await PayrollCore.GetAllAsync().ConfigureAwait(false);

                if (!productsResponse.Success || !orderItemsResponse.Success || !orderItemsResponse.Success || !clientsResponse.Success ||
                    !paymentsResponse.Success)
                {
                    return Ok(ResponseFactory.CreateResponse(false, ResponseCode.Error));
                }

                var totalIncomeValue = paymentsResponse.Data.Sum(payment => payment.Value);
                var totalOrdersValue = orderItemsResponse.Data.Sum(orderItem => orderItem.Price * orderItem.Quantity);

                var model = new SummaryModel
                {
                    NumberOfClients = clientsResponse.Data.Count,
                    NumberOfOrders = ordersResponse.Data.Count,
                    NumberOfPayments = paymentsResponse.Data.Count,
                    NumberOfProducts = productsResponse.Data.Count,
                    TotalIncomeValue = totalIncomeValue,
                    TotalOrdersValue = totalOrdersValue
                };

                return Ok(ResponseFactory<SummaryModel>.CreateResponse(true, ResponseCode.Success, model));
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<SummaryModel>.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}