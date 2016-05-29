using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class VatRepository : BaseRepository<ValueAddedTax>
    {
        protected internal VatRepository()
        {

        }

        protected internal VatRepository(Entities context) : base(context)
        {

        }
    }
}
