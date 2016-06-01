using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class CurrencyCore : BaseSinglePkCore<CurrencyRepository, Models.Currency, DataLayer.Currency>
    {
        private CurrencyCore()
        {
        }
    }
}