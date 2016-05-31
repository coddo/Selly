using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Selly.DataLayer.Repositories;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer
{
    public class DataLayerUnitOfWork : IDisposable
    {
        private static IDictionary<Type, Func<BaseDataRepository>> mRepositories;

        private Entities mContext;

        static DataLayerUnitOfWork()
        {
            InitializeUnitOfWork();
        }

        public DataLayerUnitOfWork()
        {
            if (mRepositories == null)
            {
                InitializeUnitOfWork();
            }

            mContext = new Entities();
        }

        #region Tracking Repo Factory logic

        public T TrackingRepository<T>() where T : BaseDataRepository
        {
            var type = typeof (T);
            if (!mRepositories.ContainsKey(type))
            {
                return null;
            }

            var repository = (T) mRepositories[type]();

            repository.Context = mContext;
            repository.IsEntityTrackingOn = true;

            return repository;
        }

        #endregion

        #region Non-Tracking Repo Factory logic

        public static T Repository<T>() where T : BaseDataRepository
        {
            var type = typeof (T);
            if (!mRepositories.ContainsKey(type))
            {
                return null;
            }

            return (T) mRepositories[type]();
        }

        #endregion

        #region Initialization

        private static void InitializeUnitOfWork()
        {
            mRepositories = new ConcurrentDictionary<Type, Func<BaseDataRepository>>();

            mRepositories.Add(typeof(ClientRepository), () => new ClientRepository());
            mRepositories.Add(typeof(CurrencyRepository), () => new CurrencyRepository());
            mRepositories.Add(typeof(OrderItemRepository), () => new OrderItemRepository());
            mRepositories.Add(typeof(OrderRepository), () => new OrderRepository());
            mRepositories.Add(typeof(PayrollRepository), () => new PayrollRepository());
            mRepositories.Add(typeof(ProductRepository), () => new ProductRepository());
            mRepositories.Add(typeof(VatRepository), () => new VatRepository());
        }

#endregion

        #region Disposing Logic

        public void Dispose()
        {
            if (mContext == null)
            {
                return;
            }

            mContext.Dispose();
            mContext = null;
        }

        #endregion
    }
}