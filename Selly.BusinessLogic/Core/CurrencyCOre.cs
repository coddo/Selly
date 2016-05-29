using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class CurrencyCore : BaseCore<CurrencyRepository, Models.Currency, DataLayer.Currency>
    {
        private CurrencyCore()
        {
        }

        public static CurrencyCore Instance => new CurrencyCore();
    }
}