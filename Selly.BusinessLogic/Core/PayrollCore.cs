using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;

namespace Selly.BusinessLogic.Core
{
    public class PayrollCore : BaseCore<PayrollRepository, Models.Payroll, DataLayer.Payroll>
    {
        private static PayrollCore mInstance;

        public static PayrollCore Instance => mInstance ?? (mInstance = new PayrollCore());
    }
}