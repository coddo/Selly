using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        //public static async Task<IList<TU>> GetAllAsync(IList<string> navigationProperties = null)
        //{
        //    using (var baseRepository = new T())
        //    {
                
        //    }
        //}
    }
}