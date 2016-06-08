using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;

namespace Selly.BusinessLogic.Core
{
    public class VatCore : BaseSinglePkCore<VatRepository, ValueAddedTax, DataLayer.ValueAddedTax>
    {
        private VatCore()
        {
        }
    }
}