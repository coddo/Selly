using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class ClientCore : BaseCore<ClientRepository, Models.Client, DataLayer.Client>
    {
        private static ClientCore mInstance;

        public static ClientCore Instance => mInstance ?? (mInstance = new ClientCore());
    }
}