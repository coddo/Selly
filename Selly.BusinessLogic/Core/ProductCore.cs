using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;

namespace Selly.BusinessLogic.Core
{
    public class ProductCore : BaseSinglePkCore<ProductRepository, Product, DataLayer.Product>
    {
        private ProductCore()
        {
        }
    }
}