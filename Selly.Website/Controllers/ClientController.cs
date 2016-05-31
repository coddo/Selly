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
    public class ClientController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public async Task<IHttpActionResult> Get(Guid clientId)
        {
            try
            {
                var model = await ClientCore.GetAsync(clientId, new[]
                {
                    nameof(Client.Currency)
                });

                if (model == null)
                {
                    return Ok(ResponseFactory.CreateResponse(false, HttpStatusCode.NotFound));
                }

                return Ok(ResponseFactory<Client>.CreateResponse(true, HttpStatusCode.OK, model));
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
                var modelCollection = await ClientCore.GetAllAsync(new[]
                {
                    nameof(Client.Currency)
                });

                if (modelCollection == null)
                {
                    return Ok(ResponseFactory.CreateResponse(false, HttpStatusCode.NotFound));
                }

                return Ok(ResponseFactory<IList<Client>>.CreateResponse(true, HttpStatusCode.OK, modelCollection));
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
                var model = await ClientCore.CreateAsync(client);
                if (model == null)
                {
                    return Ok(ResponseFactory.CreateResponse(false, HttpStatusCode.BadRequest));
                }

                return Ok(ResponseFactory<Client>.CreateResponse(true, HttpStatusCode.OK, model));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}