using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>
    {
        protected internal OrderItemRepository()
        {

        }

        protected internal OrderItemRepository(Entities context) : base(context)
        {

        }
    }
}
