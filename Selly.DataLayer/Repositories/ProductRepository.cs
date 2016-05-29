using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        protected internal ProductRepository()
        {

        }

        protected internal ProductRepository(Entities context) : base(context)
        {

        }
    }
}
