using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class ProductCore : BaseSinglePkCore<ProductRepository, Models.Product, DataLayer.Product>
    {
        private ProductCore()
        {
        }
    }
}