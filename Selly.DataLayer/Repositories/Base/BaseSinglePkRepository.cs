using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Selly.DataLayer.Interfaces;

namespace Selly.DataLayer.Repositories.Base
{
    public abstract class BaseSinglePkRepository<T> : BaseRepository<T>
        where T : class, ISinglePkDataAccessObject, new()
    {
        protected override Expression<Func<T, bool>> GetFindByIdQuery(IList<Guid> primaryKeys)
        {
            if (primaryKeys == null || primaryKeys.Count != 1)
            {
                return null;
            }

            return entity => entity.Id == primaryKeys[0];
        }

        protected override bool ValidateEntity(T entity)
        {
            return entity != null && entity.Id != Guid.Empty;
        }
    }
}