using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;

namespace Selly.DataAdapter
{
    public static class DasConfigurator
    {
        public static void ConfigureClients(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Client, Models.Client>().BeforeMap((source, destination) =>
            {
                source.Currency.Configure(currency => currency.Clients.Clear());
                source.Orders.Configure(order => order.Client = null);
                source.Payrolls.Configure(payroll => payroll.Client = null);
            });

            config.CreateMap<Models.Client, DataLayer.Client>().BeforeMap((source, destination) =>
            {
                source.Currency.Configure(currency => currency.Clients.Clear());
                source.Orders.Configure(order => order.Client = null);
                source.Payrolls.Configure(payroll => payroll.Client = null);
            });
        }

        public static void ConfigureCurrencies(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Currency, Models.Currency>().BeforeMap((source, destination) =>
            {
                source.Clients.Configure(client => client.Currency = null);
                source.Orders.Configure(order => order.Currency = null);
            });

            config.CreateMap<Models.Currency, DataLayer.Currency>().BeforeMap((source, destination) =>
            {
                source.Clients.Configure(client => client.Currency = null);
                source.Orders.Configure(order => order.Currency = null);
            });
        }

        public static void ConfigureOrders(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Order, Models.Order>().BeforeMap((source, destination) =>
            {
                source.Currency.Configure(currency => currency.Orders.Clear());
                source.Client.Configure(client => client.Orders.Clear());
                source.OrderItems.Configure(orderItem => orderItem.Order = null);
                source.Payrolls.Configure(payroll => payroll.Order = null);
            });

            config.CreateMap<Models.Order, DataLayer.Order>().BeforeMap((source, destination) =>
            {
                source.Currency.Configure(currency => currency.Orders.Clear());
                source.Client.Configure(client => client.Orders.Clear());
                source.OrderItems.Configure(orderItem => orderItem.Order = null);
                source.Payrolls.Configure(payroll => payroll.Order = null);
            });
        }

        public static void ConfigureOrderItems(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.OrderItem, Models.OrderItem>().BeforeMap((source, destination) =>
            {
                source.Order.Configure(order => order.OrderItems.Clear());
                source.Product.Configure(product => product.OrderItems.Clear());
            });

            config.CreateMap<Models.OrderItem, DataLayer.OrderItem>().BeforeMap((source, destination) =>
            {
                source.Order.Configure(order => order.OrderItems.Clear());
                source.Product.Configure(product => product.OrderItems.Clear());
            });
        }

        public static void ConfigurePayrolls(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Payroll, Models.Payroll>().BeforeMap((source, destination) =>
            {
                source.Order.Configure(order => order.Payrolls.Clear());
                source.Client.Configure(client => client.Payrolls.Clear());
            });

            config.CreateMap<Models.Payroll, DataLayer.Payroll>().BeforeMap((source, destination) =>
            {
                source.Order.Configure(order => order.Payrolls.Clear());
                source.Client.Configure(client => client.Payrolls.Clear());
            });
        }

        public static void ConfigureProducts(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Product, Models.Product>().BeforeMap((source, destination) =>
            {
                source.ValueAddedTax.Configure(vat => vat.Products.Clear());
                source.OrderItems.Configure(orderItem => orderItem.Product = null);
            });

            config.CreateMap<Models.Product, DataLayer.Product>().BeforeMap((source, destination) =>
            {
                source.ValueAddedTax.Configure(vat => vat.Products.Clear());
                source.OrderItems.Configure(orderItem => orderItem.Product = null);
            });
        }

        public static void ConfigureVats(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.ValueAddedTax, Models.ValueAddedTax>()
                  .BeforeMap((source, destination) => { source.Products.Configure(product => product.ValueAddedTax = null); });

            config.CreateMap<Models.ValueAddedTax, DataLayer.ValueAddedTax>()
                  .BeforeMap((source, destination) => { source.Products.Configure(product => product.ValueAddedTax = null); });
        }

        #region Item configuration extension methods

        private static void Configure<T>(this IEnumerable<T> items, Action<T> applyConfiguration)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                applyConfiguration(item);
            }
        }

        private static void Configure<T>(this T item, Action<T> applyConfiguration)
        {
            if (item == null)
            {
                return;
            }

            applyConfiguration(item);
        }

        #endregion
    }
}