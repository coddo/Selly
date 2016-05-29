using System;

namespace Selly.DataLayer.Repositories.Base
{
    public class BaseDataRepository : IDisposable
    {
        protected BaseDataRepository(Entities context)
        {
            Context = context;
            Context.Configuration.LazyLoadingEnabled = false;
        }

        protected Entities Context { get; }

        #region Disposing logic

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}