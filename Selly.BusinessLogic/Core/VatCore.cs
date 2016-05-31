using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class VatCore : BaseCore<VatRepository, Models.ValueAddedTax, DataLayer.ValueAddedTax>
    {
        private VatCore()
        {
        }
    }
}