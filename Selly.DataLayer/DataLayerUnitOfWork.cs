using System;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer
{
    public class DataLayerUnitOfWork : IDisposable
    {
        private Entities mContext;

        public DataLayerUnitOfWork()
        {
            mContext = new Entities();
        }

        #region Tracking Repo Factory logic


        #endregion

        #region Non-Tracking Repo Factory logic

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