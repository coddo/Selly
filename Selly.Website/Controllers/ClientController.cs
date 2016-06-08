using System;
using System.Threading.Tasks;
using System.Web.Http;
using LoggingService;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

namespace Selly.Website.Controllers
{
    public class ClientController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public async Task<IHttpActionResult> Get(Guid clientId)
        {
            try
            {
                var response = await ClientCore.GetAsync(clientId, new[]
                {
                    nameof(Client.Currency)
                });

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<ClientController>(e);

                return Ok(ResponseFactory.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var response = await ClientCore.GetAllAsync(new[]
                {
                    nameof(Client.Currency)
                });

                return Ok(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<ClientController>(e);

                return Ok(ResponseFactory.CreateResponse(false, ResponseCode.Error));
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IHttpActionResult> Create([FromBody] Client client)
        {
            try
            {
                var response = await ClientCore.CreateAsync(client).ConfigureAwait(false);

                return Json(response);
            }
            catch (Exception e)
            {
                LogHelper.LogException<ClientController>(e);

                return Ok(ResponseFactory.CreateResponse(false, ResponseCode.Error));
            }
        }
    }
}