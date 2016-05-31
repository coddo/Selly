using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.DataLayer.Repositories;
using Selly.Models;
using Selly.Models.Common.ClientServerInteraction;

namespace Selly.BusinessLogic.Core
{
    public class OrderCore : BaseCore<OrderRepository, Models.Order, DataLayer.Order>
    {
        private OrderCore()
        {
        }
    }
}