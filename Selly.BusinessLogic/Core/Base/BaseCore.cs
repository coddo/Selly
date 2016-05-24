using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.DataAdapter;
using Selly.DataLayer.Interfaces;
using Selly.DataLayer.Repositories.Base;
using Selly.Models.Interfaces;

namespace Selly.BusinessLogic.Core.Base
{
    public class BaseCore<TRepo, TModel, TEntity>
        where TRepo : BaseRepository<TEntity>, new()
        where TEntity : class, IDataAccessObject, new()
        where TModel : class, IModel, new()
    {
        public static async Task<IList<TModel>> GetAllAsync(IList<string> navigationProperties = null)
        {
            using (var repository = new TRepo())
            {
                var entities = await repository.GetAllAsync(navigationProperties);

                return entities.CopyTo<TModel>();
            }
        }

        public async Task<TModel> GetAsync(Guid id, IList<string> navigationProperties = null)
        {
            using (var repository = new TRepo())
            {
                var entities = await repository.GetAsync(id, navigationProperties);

                return entities.CopyTo<TModel>();
            }
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            using (var repository = new TRepo())
            {
                var entity = model.CopyTo<TEntity>();

                entity = await repository.CreateAsync(entity);

                return entity.CopyTo<TModel>();
            }
        }

        public virtual async Task<IList<TModel>> CreateAsync(IList<TModel> modelCollection)
        {
            using (var repository = new TRepo())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                entityCollection = await repository.CreateAsync(entityCollection);

                return entityCollection.CopyTo<TModel>();
            }
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            using (var repository = new TRepo())
            {
                var entity = model.CopyTo<TEntity>();

                entity = await repository.UpdateAsync(entity);

                return entity.CopyTo<TModel>();
            }
        }

        public virtual async Task<IList<TModel>> UpdateAsync(IList<TModel> modelCollection)
        {
            using (var repository = new TRepo())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                entityCollection = await repository.UpdateAsync(entityCollection);

                return entityCollection.CopyTo<TModel>();
            }
        }

        public virtual async Task<bool> DeleteAsync(TModel model)
        {
            using (var repository = new TRepo())
            {
                var entity = model.CopyTo<TEntity>();

                return await repository.DeleteAsync(entity);
            }
        }

        public virtual async Task<bool> DeleteAsync(IList<TModel> modelCollection)
        {
            using (var repository = new TRepo())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                return await repository.DeleteAsync(entityCollection);
            }
        }
    }
}