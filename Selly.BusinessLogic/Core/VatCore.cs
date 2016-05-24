using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class VatCore : BaseCore<VatRepository, Models.ValueAddedTax, DataLayer.ValueAddedTax>
    {
        private static VatCore mInstance;

        public static VatCore Instance => mInstance ?? (mInstance = new VatCore());
    }
}