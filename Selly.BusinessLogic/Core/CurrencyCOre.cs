using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;

namespace Selly.BusinessLogic.Core
{
    public class CurrencyCore : BaseSinglePkCore<CurrencyRepository, Currency, DataLayer.Currency>
    {
        private CurrencyCore()
        {
        }
    }
}