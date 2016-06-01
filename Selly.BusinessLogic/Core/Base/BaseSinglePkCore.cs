using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.DataAdapter;
using Selly.DataLayer;
using Selly.DataLayer.Interfaces;
using Selly.DataLayer.Repositories.Base;
using Selly.Models.Interfaces;

namespace Selly.BusinessLogic.Core.Base
{
    public abstract class BaseSinglePkCore<TRepo, TModel, TEntity> : BaseCore<TRepo, TModel, TEntity>
        where TRepo : BaseSinglePkRepository<TEntity>
        where TEntity : class, ISinglePkDataAccessObject, new()
        where TModel : class, ISinglePkModel, new()
    {
        public static async Task<TModel> GetAsync(Guid id, IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entities = await repository.GetAsync(new[]
                {
                    id
                }, navigationProperties);

                return entities.CopyTo<TModel>();
            }
        }
    }
}