using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class OrderRepository : BaseSinglePkRepository<Order>
    {
        public async Task<IList<Order>> GetByClientId(Guid clientId, IList<string> navigationProperties = null)
        {
            var orders = await FetchListAsync(order => order.ClientId == clientId, navigationProperties).ConfigureAwait(false);

            return orders;
        }
    }
}
