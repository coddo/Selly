using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Selly.DataLayer.Interfaces;

namespace Selly.DataLayer.Repositories
{
    public abstract class BaseSinglePkRepository<T> : BaseRepository<T>
        where T : class, ISinglePkDataAccessObject, new()
    {
        public async Task<T> GetAsync(Guid id, IList<string> navigationProperties = null)
        {
            return await FetchSingleAsync(entity => entity.Id == id, navigationProperties).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id).ConfigureAwait(false);
            if (!ValidateEntity(entity))
            {
                return false;
            }

            await RemoveAsync(entity).ConfigureAwait(false);

            return true;
        }

        #region Overrides

        public override Task<T> CreateAsync(T entity, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            return base.CreateAsync(entity, refreshFromDb, navigationProperties);
        }

        public override Task<IList<T>> CreateAsync(IList<T> entities, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            Parallel.ForEach(entities.Where(entity => entity.Id == Guid.Empty), entity => { entity.Id = Guid.NewGuid(); });

            return base.CreateAsync(entities, refreshFromDb, navigationProperties);
        }

        protected override async Task<T> FetchFromDbAsync(T entity, IList<string> navigationProperties = null)
        {
            return await GetAsync(entity.Id, navigationProperties).ConfigureAwait(false);
        }

        protected override bool ValidateEntity(T entity)
        {
            if (entity != null && entity.Id != Guid.Empty)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}