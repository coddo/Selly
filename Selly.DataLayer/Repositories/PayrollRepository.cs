using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class PayrollRepository : BaseRepository<Payroll>
    {
        protected internal PayrollRepository()
        {

        }

        protected internal PayrollRepository(Entities context) : base(context)
        {

        }
    }
}
