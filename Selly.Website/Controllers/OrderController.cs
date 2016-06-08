using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

namespace Selly.Website.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public async Task<IHttpActionResult> Get(Guid orderId)
        {
            try
            {
                var result = await OrderCore.GetAsync(orderId, new[]
                {
                    nameof(Order.Client),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.ValueAddedTax)}",
                    nameof(Order.OrderItems),
                    nameof(Order.Payrolls),
                    nameof(Order.Currency)
                }).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Order>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll(string orderBy = "", bool orderAscending = true)
        {
            try
            {
                var response = await OrderCore.GetAllAsync(orderBy, orderAscending, new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.ValueAddedTax)}",
                    nameof(Order.Payrolls),
                    nameof(Order.Currency)
                }).ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Order>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetAllForUser")]
        public async Task<IHttpActionResult> GetAllForUser(Guid userId, string orderBy = "", bool orderAscending = true)
        {
            try
            {
                var result = await OrderCore.GetAllForUserAsync(userId, orderBy, orderAscending, new[]
                {
                    nameof(Order.Client),
                    nameof(Order.OrderItems),
                    nameof(Order.Payrolls),
                    nameof(Order.Currency)
                }).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<IList<Order>>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Order order)
        {
            try
            {
                var result = await OrderCore.CreateAsync(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IHttpActionResult> Update([FromBody] Order order)
        {
            try
            {
                var result = await OrderCore.UpdateAsync(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        [ActionName("Finalize")]
        public async Task<IHttpActionResult> Finalize([FromBody] Order order)
        {
            try
            {
                var result = await OrderCore.Finalize(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        [ActionName("Cancel")]
        public async Task<IHttpActionResult> Cancel([FromBody] Order order)
        {
            try
            {
                var result = await OrderCore.Cancel(order).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error));
            }
        }

        // THIS HAD NO TIME LEFT TO IMPLEMENT
        [HttpGet]
        [ActionName("GetInvoice")]
        public async Task<IHttpActionResult> GetInvoice(Guid orderId)
        {
            try
            {
                // A PDF/CSV FILE WAS INTENDED TO BE DOWNLOADED BASED ON THE ORDER, ORDERITEMS AND PAYROLL
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}