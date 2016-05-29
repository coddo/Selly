using System;
using System.Collections.Generic;
using Selly.DataLayer.Repositories;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer
{
    public class DataLayerUnitOfWork : IDisposable
    {
        private static readonly IDictionary<Type, Func<BaseDataRepository>> mRepositories = new Dictionary<Type, Func<BaseDataRepository>>
        {
            {
                typeof (ClientRepository), () => new ClientRepository()
            }
        };

        private Entities mContext;

        public DataLayerUnitOfWork()
        {
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
            var type = typeof(T);
            if (!mRepositories.ContainsKey(type))
            {
                return null;
            }

            return (T) mRepositories[type]();
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