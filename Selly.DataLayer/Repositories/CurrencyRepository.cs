using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>
    {
        protected internal CurrencyRepository()
        {

        }

        protected internal CurrencyRepository(Entities context) : base(context)
        {

        }
    }
}
