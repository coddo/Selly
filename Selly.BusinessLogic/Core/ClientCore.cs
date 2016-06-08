using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;

namespace Selly.BusinessLogic.Core
{
    public class ClientCore : BaseSinglePkCore<ClientRepository, Client, DataLayer.Client>
    {
        private ClientCore()
        {
        }
    }
}