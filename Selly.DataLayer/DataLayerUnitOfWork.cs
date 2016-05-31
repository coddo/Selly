using System;
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
            mRepositories = new Dictionary<Type, Func<BaseDataRepository>>
            {
                {
                    typeof (ClientRepository), () => new ClientRepository()
                },
                {
                    typeof (CurrencyRepository), () => new CurrencyRepository()
                },
                {
                    typeof (OrderItemRepository), () => new OrderItemRepository()
                },
                {
                    typeof (OrderRepository), () => new OrderRepository()
                },
                {
                    typeof (PayrollRepository), () => new PayrollRepository()
                },
                {
                    typeof (ProductRepository), () => new ProductRepository()
                },
                {
                    typeof (VatRepository), () => new VatRepository()
                }
            };
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