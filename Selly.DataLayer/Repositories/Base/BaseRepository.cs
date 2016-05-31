﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Selly.DataLayer.Interfaces;

namespace Selly.DataLayer.Repositories.Base
{
    public abstract class BaseRepository<T> : GenericDataRepository<T>
        where T : class, IDataAccessObject, new()
    {
        internal BaseRepository()
        {
        }

        public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> query, IList<string> navigationProperties = null)
        {
            return await FetchListAsync(query, navigationProperties);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> query, IList<string> navigationProperties = null)
        {
            return await FetchSingleAsync(query, navigationProperties);
        }

        public virtual async Task<IList<T>> GetAllAsync(IList<string> navigationProperties = null)
        {
            return await FetchAllAsync(navigationProperties);
        }

        public virtual async Task<T> GetAsync(Guid id, IList<string> navigationProperties = null)
        {
            return await FetchSingleAsync(entity => entity.Id == id, navigationProperties);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                return null;
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            return await AddAsync(entity);
        }

        public virtual async Task<IList<T>> CreateAsync(IList<T> entities)
        {
            if (entities.Any(e => e == null))
            {
                return null;
            }

            foreach (var entity in entities.Where(entity => entity.Id == Guid.Empty))
            {
                entity.Id = Guid.NewGuid();
            }

            return await AddAsync(entities);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null || entity.Id == Guid.Empty)
            {
                return null;
            }

            return await ChangeAsync(entity);
        }

        public virtual async Task<IList<T>> UpdateAsync(IList<T> entities)
        {
            if (entities.Any(entity => entity == null || entity.Id == Guid.Empty))
            {
                return null;
            }

            return await ChangeAsync(entities);
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null || entity.Id == Guid.Empty)
            {
                return false;
            }

            await RemoveAsync(entity);

            return true;
        }

        public virtual async Task<bool> DeleteAsync(IList<T> entities)
        {
            if (entities == null || entities.Any(entity => entity == null || entity.Id == Guid.Empty))
            {
                return false;
            }

            await RemoveAsync(entities);

            return true;
        }
    }
}