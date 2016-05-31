using System.Threading.Tasks;
using Selly.BusinessLogic.Core;
using Selly.Models;
using System;
using System.Linq;

namespace Selly.BusinessLogic.Service
{
    public class MockDataInitializationService
    {
        private readonly Product[] mProducts;
        private readonly Client[] mClients;

        private static MockDataInitializationService mInstance;

        private MockDataInitializationService()
        {
            mProducts = new[]
            {
                new Product
                {
                    Name = "Ceas de mana",
                    Price = 199,
                    VatId = Guid.Parse("B3312938-75E0-4711-AF84-A66750724EB1")
                },
                new Product
                {
                    Name = "Paine neagra",
                    Price = 2.34,
                    VatId = Guid.Parse("3F659D46-9AF8-4E7D-8A0C-D6EDF1218BB9")
                },
                new Product
                {
                    Name = "Iaurt",
                    Price = 2.86,
                    VatId = Guid.Parse("0BE69C70-ADF1-4321-B022-20583F207526")
                },
                new Product
                {
                    Name = "Ochelari de soare",
                    Price = 26.99,
                    VatId = Guid.Parse("B3312938-75E0-4711-AF84-A66750724EB1")
                },
                new Product
                {
                    Name = "Cioco Milka",
                    Price = 5.23,
                    VatId = Guid.Parse("0BE69C70-ADF1-4321-B022-20583F207526")
                },
            };

            mClients = new[]
            {
                new Client
                {
                    Email = "client1@email.com",
                    FirstName = "Client-First1",
                    LastName = "Client-Last1"
                },
                new Client
                {
                    Email = "client2@email.com",
                    FirstName = "Client-First2",
                    LastName = "Client-Last2"
                },
                new Client
                {
                    Email = "client3@email.com",
                    FirstName = "Client-First3",
                    LastName = "Client-Last3"
                },
                new Client
                {
                    Email = "client4@email.com",
                    FirstName = "Client-First4",
                    LastName = "Client-Last4"
                },
            };
        }

        public static MockDataInitializationService Instance => mInstance ?? (mInstance = new MockDataInitializationService());

        public void InitializeMockData()
        {
            Task.Run(async () =>
            {
                var existingProducts = await ProductCore.GetAllAsync().ConfigureAwait(false);
                if (existingProducts?.Count != 0)
                {
                    return;
                }
                await ProductCore.CreateAsync(mProducts);

                var existingClients = await ClientCore.GetAllAsync().ConfigureAwait(false);
                if (existingClients?.Count != 0)
                {
                    return;
                }

                var currencies = await CurrencyCore.GetAllAsync().ConfigureAwait(false);
                var currency = currencies.FirstOrDefault(c => c.Name == "RON");
                foreach (var client in mClients)
                {
                    client.CurrencyId = currency.Id;
                }

                await ClientCore.CreateAsync(mClients).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}