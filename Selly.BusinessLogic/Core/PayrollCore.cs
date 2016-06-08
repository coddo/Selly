using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.DataAdapter;
using Selly.DataLayer.Extensions;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;

namespace Selly.BusinessLogic.Core
{
    public class PayrollCore : BaseSinglePkCore<PayrollRepository, Payroll, DataLayer.Payroll>
    {
        private PayrollCore()
        {
        }

        public static async Task<Payroll> GetForOrder(Guid orderId, IList<string> navigationProperties = null)
        {
            using (var payrollRepository = DataLayerUnitOfWork.Repository<PayrollRepository>())
            {
                var payroll = await payrollRepository.GetForOrder(orderId, navigationProperties).ConfigureAwait(false);

                return payroll.CopyTo<Payroll>();
            }
        }

        public static async Task<IList<Payroll>> GetAllForClient(Guid clientId, IList<string> navigationProperties = null)
        {
            using (var payrollRepository = DataLayerUnitOfWork.Repository<PayrollRepository>())
            {
                var payrolls = await payrollRepository.GetAllForClient(clientId, navigationProperties).ConfigureAwait(false);

                return payrolls.CopyTo<Payroll>();
            }
        }
    }
}