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
    internal class BaseDoublePkCore<TRepo, TModel, TEntity> : BaseCore<TRepo, TModel, TEntity>
        where TRepo : BaseRepository<TEntity>
        where TEntity : class, IDataAccessObject, new()
        where TModel : class, IModel, new()
    {
        public static async Task<TModel> GetAsync(Guid firstId, Guid secondId, IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entities = await repository.GetAsync(new[]
                {
                    firstId,
                    secondId
                }, navigationProperties);

                return entities.CopyTo<TModel>();
            }
        }
    }
}