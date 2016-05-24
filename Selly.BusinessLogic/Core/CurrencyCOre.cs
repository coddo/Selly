using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class CurrencyCore : BaseCore<CurrencyRepository, Models.Currency, DataLayer.Currency>
    {
        private static CurrencyCore mInstance;

        public static CurrencyCore Instance => mInstance ?? (mInstance = new CurrencyCore());
    }
}