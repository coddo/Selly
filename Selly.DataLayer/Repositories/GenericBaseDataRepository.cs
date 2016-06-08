using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Selly.DataLayer.Interfaces;

namespace Selly.DataLayer.Repositories
{
    public abstract class GenericDataRepository<T> : BaseDataRepository
        where T : class, IDataAccessObject, new()
    {
        private DbSet<T> mDbSet;
        private bool mIsEntityTrackingOn;
        private Func<IList<string>, IQueryable<T>> mQueryGenerator;

        protected GenericDataRepository()
        {
            Context = new Entities();
            IsEntityTrackingOn = false;
        }

        #region Overrides

        public override sealed bool IsEntityTrackingOn
        {
            get { return mIsEntityTrackingOn; }
            set
            {
                mIsEntityTrackingOn = value;

                mQueryGenerator = mIsEntityTrackingOn ? (Func<IList<string>, IQueryable<T>>) GenerateQuery : GenerateNonTrackingQuery;
            }
        }

        public override sealed Entities Context
        {
            get { return base.Context; }
            set
            {
                base.Context = value;

                mDbSet = value.Set<T>();
            }
        }

        #endregion

        #region Protected methods

        protected async Task<IList<T>> FetchAllAsync(IList<string> navigationProperties = null)
        {
            var dbQuery = mQueryGenerator.Invoke(navigationProperties);

            var list = await dbQuery.ToListAsync().ConfigureAwait(false);
            return list;
        }

        protected async Task<IList<T>> FetchListAsync(Expression<Func<T, bool>> where, IList<string> navigationProperties = null)
        {
            var dbQuery = mQueryGenerator.Invoke(navigationProperties);

            var list = await dbQuery.Where(@where).ToListAsync().ConfigureAwait(false);

            return list;
        }

        protected async Task<T> FetchSingleAsync(Expression<Func<T, bool>> where, IList<string> navigationProperties = null)
        {
            var dbQuery = mQueryGenerator.Invoke(navigationProperties);

            var item = await dbQuery.FirstOrDefaultAsync(@where).ConfigureAwait(false);

            return item;
        }

        #endregion

        #region Internal methods

        internal async Task<T> AddAsync(T item)
        {
            mDbSet.Add(item);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return item;
        }

        internal async Task<IList<T>> AddAsync(IList<T> items)
        {
            mDbSet.AddRange(items);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return items;
        }

        internal async Task<T> ChangeAsync(T item)
        {
            Context.Entry(item).State = EntityState.Modified;

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return item;
        }

        internal async Task<IList<T>> ChangeAsync(IList<T> items)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            Context.Configuration.AutoDetectChangesEnabled = true;

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return items;
        }

        internal async Task RemoveAsync(T item)
        {
            mDbSet.Remove(item);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        internal async Task RemoveAsync(IList<T> items)
        {
            mDbSet.RemoveRange(items);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion

        #region Private methods

        private IQueryable<T> GenerateNonTrackingQuery(IList<string> navigationProperties)
        {
            IQueryable<T> dbQuery = mDbSet.AsNoTracking();

            return ApplyNavigationProperties(dbQuery, navigationProperties);
        }

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
    }
}
