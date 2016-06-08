using System;
using System.Threading.Tasks;
using System.Web.Http;
using Selly.BusinessLogic.Core;
using Selly.Models;

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
                return InternalServerError(e);
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
                return InternalServerError(e);
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
                return InternalServerError(e);
            }
        }
    }
}