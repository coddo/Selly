using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class PayrollCore : BaseCore<PayrollRepository, Models.Payroll, DataLayer.Payroll>
    {
        private PayrollCore()
        {
        }
    }
}