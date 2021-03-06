﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core;
using Selly.Models;
using System.Collections.Generic;

namespace Selly.BusinessLogic.Service
{
    public class MockDataInitializationService
    {
        private static MockDataInitializationService mInstance;
        private readonly Client[] mClients;
        private readonly Product[] mProducts;

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
                new Product
                {
                    Name = "Biscuiti",
                    Price = 3.1415,
                    VatId = Guid.Parse("0BE69C70-ADF1-4321-B022-20583F207526")
                }
            };

            mClients = new[]
            {
                new Client
                {
                    Email = "client1@email.com",
                    FirstName = "John",
                    LastName = "Titor"
                },
                new Client
                {
                    Email = "client2@email.com",
                    FirstName = "Tomoya",
                    LastName = "Okazaki"
                },
                new Client
                {
                    Email = "client3@email.com",
                    FirstName = "Ion",
                    LastName = "Popescu"
                },
                new Client
                {
                    Email = "client4@email.com",
                    FirstName = "Maria",
                    LastName = "Ionescu"
                }
            };
        }

        public static MockDataInitializationService Instance => mInstance ?? (mInstance = new MockDataInitializationService());

        public void InitializeMockData()
        {
            try
            {
                Task.Run(async () =>
                {
                    var existingProducts = await ProductCore.GetAllAsync().ConfigureAwait(false);
                    if (existingProducts.Data != null && existingProducts.Data.Count != 0)
                    {
                        return;
                    }

                    var products = await ProductCore.CreateAsync(mProducts, true).ConfigureAwait(false);

                    var existingClients = await ClientCore.GetAllAsync().ConfigureAwait(false);
                    if (existingClients.Data != null && existingClients.Data.Count != 0)
                    {
                        return;
                    }

                    var currencies = await CurrencyCore.GetAllAsync().ConfigureAwait(false);
                    if (!currencies.Success || currencies.Data == null || currencies.Data.Count == 0)
                    {
                        return;
                    }

                    var currency = currencies.Data.FirstOrDefault(c => c.Name == "RON");
                    if (currency == null)
                    {
                        return;
                    }

                    foreach (var client in mClients)
                    {
                        client.CurrencyId = currency.Id;
                    }

                    var clients = await ClientCore.CreateAsync(mClients, true).ConfigureAwait(false);

                    var existingOrders = await OrderCore.GetAllAsync().ConfigureAwait(false);
                    if (existingOrders.Data != null && existingOrders.Data.Count != 0)
                    {
                        return;
                    }

                    int nr = 0;
                    List<Order> newOrders = new List<Order>();
                    foreach (var client in clients.Data)
                    {
                        if (products.Data.Count >= 6)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                nr++;
                                var o = new Order()
                                {
                                    Id = Guid.NewGuid(),
                                    CurrencyId = currency.Id,
                                    ClientId = client.Id,
                                    SaleType = i,
                                    Status = 0,
                                    Date = DateTime.Now.AddDays(-nr).AddHours(-nr),
                                    OrderItems = new List<OrderItem>()
                                };

                                for (int k = i + nr / 2; k < 6; k += 1)
                                {
                                    o.OrderItems.Add(new OrderItem()
                                    {
                                        Id = Guid.NewGuid(),
                                        OrderId = o.Id,
                                        ProductId = products.Data.ElementAt(k).Id,
                                        Price = products.Data.ElementAt(k).Price,
                                        Quantity = k + 1
                                    });
                                }

                                newOrders.Add(o);
                            }
                        }
                    }

                    var orders = await OrderCore.CreateAsync(newOrders, true, new[] { nameof(Order.OrderItems) }).ConfigureAwait(false);

                    var payrolls = new List<Payroll>();
                    if (orders.Data.Count > 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var payroll = new Payroll()
                            {
                                Id = Guid.NewGuid(),
                                ClientId = clients.Data.ElementAt(i).Id,
                                OrderId = orders.Data.ElementAt(i).Id,
                                Date = DateTime.Now.AddDays(-i),
                                Value = orders.Data.ElementAt(i).OrderItems.Sum(p => p.Price * p.Quantity),
                            };

                            payrolls.Add(payroll);
                        }
                    }

                    await PayrollCore.CreateAsync(payrolls, true).ConfigureAwait(false);

                }).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}