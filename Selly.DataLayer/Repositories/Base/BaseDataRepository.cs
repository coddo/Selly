using System;

namespace Selly.DataLayer.Repositories.Base
{
    public abstract class BaseDataRepository : IDisposable
    {
        private Entities mContext;

        protected internal abstract bool IsEntityTrackingOn { get; set; }

        protected internal virtual Entities Context
        {
            get
            {
                return mContext;
            }
            set
            {
                if (value == null)
                {
                    //LogHelper.LogException<DataRepository>("Could not create the database context.");
                    throw new NullReferenceException("Tried to use repository with null context");
                }

                value.Configuration.LazyLoadingEnabled = false;
                mContext = value;
            }
        }

        #region Disposing logic

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}