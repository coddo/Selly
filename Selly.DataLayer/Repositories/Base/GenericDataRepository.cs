﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Selly.DataLayer.Interfaces;

namespace Selly.DataLayer.Repositories.Base
{
    public abstract class GenericDataRepository<T> : IDisposable
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> mDbSet;

        protected GenericDataRepository()
        {
            Context = new Entities();
            mDbSet = Context.Set<T>();
        }

        protected Entities Context { get; }

        protected async Task<IList<T>> FetchAllAsync(IList<string> navigationProperties = null)
        {
            var dbQuery = GenerateQuery(navigationProperties);

            var list = await dbQuery.ToListAsync();
            return list;
        }

        protected async Task<IList<T>> FetchListAsync(Expression<Func<T, bool>> where, IList<string> navigationProperties = null)
        {
            var dbQuery = GenerateQuery(navigationProperties);

            var list = await dbQuery.Where(@where).ToListAsync();

            return list;
        }

        protected async Task<T> FetchSingleAsync(Expression<Func<T, bool>> where, IList<string> navigationProperties = null)
        {
            var dbQuery = GenerateQuery(navigationProperties);

            var item = await dbQuery.FirstOrDefaultAsync(@where);

            return item;
        }

        protected async Task<T> AddAsync(T item)
        {
            mDbSet.Add(item);

            await Context.SaveChangesAsync();

            return item;
        }

        protected async Task<IList<T>> AddAsync(IList<T> items)
        {
            mDbSet.AddRange(items);

            await Context.SaveChangesAsync();

            return items;
        }

        protected async Task<T> ChangeAsync(T item)
        {
            Context.Entry(item).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return item;
        }

        protected async Task<IList<T>> ChangeAsync(IList<T> items)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            await Context.SaveChangesAsync();

            return items;
        }

        protected async Task RemoveAsync(T item)
        {
            mDbSet.Remove(item);

            await Context.SaveChangesAsync();
        }

        protected async Task RemoveAsync(IEnumerable<T> items)
        {
            mDbSet.RemoveRange(items);

            await Context.SaveChangesAsync();
        }

        #region Private methods

        private IQueryable<T> GenerateQuery(IList<string> navigationProperties)
        {
            IQueryable<T> dbQuery = mDbSet;

            return ApplyNavigationProperties(dbQuery, navigationProperties);
        }

        private static IQueryable<T> ApplyNavigationProperties(IQueryable<T> dbQuery, IList<string> navigationProperties)
        {
            if (navigationProperties != null)
            {
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            }

            return dbQuery;
        }

        #endregion

        #region Disposing logic

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}