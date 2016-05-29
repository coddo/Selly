using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        protected internal OrderRepository()
        {

        }

        protected internal OrderRepository(Entities context) : base(context)
        {

        }
    }
}
