using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class ClientCore : BaseSinglePkCore<ClientRepository, Models.Client, DataLayer.Client>
    {
        private ClientCore()
        {
        }
    }
}