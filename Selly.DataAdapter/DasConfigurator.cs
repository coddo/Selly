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
            config.CreateMap<DataLayer.Client, Models.Client>();
            config.CreateMap<Models.Client, DataLayer.Client>();
        }

        public static void ConfigureCurrencies(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Currency, Models.Currency>();
            config.CreateMap<Models.Currency, DataLayer.Currency>();
        }

        public static void ConfigureOrders(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Order, Models.Order>();
            config.CreateMap<Models.Order, DataLayer.Order>();
        }

        public static void ConfigureOrderItems(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.OrderItem, Models.OrderItem>();
            config.CreateMap<Models.OrderItem, DataLayer.OrderItem>();
        }

        public static void ConfigurePayrolls(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Payroll, Models.Payroll>();
            config.CreateMap<Models.Payroll, DataLayer.Payroll>();
        }

        public static void ConfigureProducts(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.Product, Models.Product>();
            config.CreateMap<Models.Product, DataLayer.Product>();
        }

        public static void ConfigureVats(IMapperConfiguration config)
        {
            config.CreateMap<DataLayer.ValueAddedTax, Models.ValueAddedTax>();
            config.CreateMap<Models.ValueAddedTax, DataLayer.ValueAddedTax>();
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