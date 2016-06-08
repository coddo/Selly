using Ploeh.AutoFixture;
using Selly.Models;

namespace Selly.BusinessLogic.Tests
{
    public static partial class EntityHelper
    {
        private static readonly Fixture mFixture = new Fixture { Behaviors = { new OmitOnRecursionBehavior() } };

        public static Client GenerateClient()
        {
            return mFixture.Build<Client>()
                .Without(client => client.Currency)
                .Without(client => client.Orders)
                .Without(client => client.Payrolls)
                .Create();
        }

        public static Currency GenerateCurreny()
        {
            return mFixture.Build<Currency>()
                .Without(client => client.Clients)
                .Without(client => client.Orders)
                .Create();
        }
    }
}
