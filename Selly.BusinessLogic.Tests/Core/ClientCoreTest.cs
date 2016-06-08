using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selly.BusinessLogic.Core;
using Selly.Models;
using Selly.Models.Common.Response;

namespace Selly.BusinessLogic.Tests.Core
{
    [TestClass]
    public class ClientCoreTest
    {
        private static Client mClient;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            CreateClient();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DeleteClient();
        }

        [TestMethod]
        public void UpdateClientTest()
        {
            var newClient = EntityHelper.GenerateClient();
            newClient.Id = mClient.Id;

            var response = ClientCore.UpdateAsync(newClient, true).ConfigureAwait(false).GetAwaiter().GetResult();

            AssertObjects(response, newClient);
        }

        [TestMethod]
        public void GetClientTest()
        {
            var response = ClientCore.GetAsync(mClient.Id).ConfigureAwait(false).GetAwaiter().GetResult();

            AssertObjects(response, mClient);
        }

        [TestMethod]
        public void GetAllClientsTest()
        {
            var response = ClientCore.GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Data.Count.Should().NotBe(0);
        }

        private static void CreateClient()
        {
            mClient = EntityHelper.GenerateClient();

            var response = ClientCore.CreateAsync(mClient, true).ConfigureAwait(false).GetAwaiter().GetResult();

            AssertObjects(response, mClient);
        }

        private static void DeleteClient()
        {
            var response = ClientCore.DeleteAsync(mClient).ConfigureAwait(false).GetAwaiter().GetResult();

            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        private static void AssertObjects(Response<Client> response, Client client)
        {
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Data.CurrencyId.Should().Be(client.CurrencyId);
            response.Data.Email.Should().Be(client.Email);
            response.Data.FirstName.Should().Be(client.FirstName);
            response.Data.LastName.Should().Be(client.LastName);
            response.Data.Id.Should().Be(client.Id);
        }
    }
}
