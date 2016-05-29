using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class ProductCore : BaseCore<ProductRepository, Models.Product, DataLayer.Product>
    {
        private ProductCore()
        {
        }

        public static ProductCore Instance => new ProductCore();
    }
}