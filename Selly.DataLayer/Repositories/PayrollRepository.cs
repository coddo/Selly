using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class PayrollRepository : BaseSinglePkRepository<Payroll>
    {
        public async Task<Payroll> GetForOrder(Guid orderId, IList<string> navigationProperties = null)
        {
            return await FetchSingleAsync(payroll => payroll.OrderId == orderId, navigationProperties).ConfigureAwait(false);
        }

        public async Task<IList<Payroll>> GetAllForClient(Guid clientId, IList<string> navigationProperties = null)
        {
            return await FetchListAsync(payroll => payroll.ClientId == clientId, navigationProperties).ConfigureAwait(false);
        }
    }
}