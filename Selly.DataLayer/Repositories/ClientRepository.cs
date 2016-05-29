using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        protected internal ClientRepository()
        {

        }

        protected internal ClientRepository(Entities context) : base(context)
        {

        }
    }
}
