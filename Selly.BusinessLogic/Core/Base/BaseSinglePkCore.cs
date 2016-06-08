using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Selly.DataAdapter;
using Selly.DataLayer.Extensions;
using Selly.DataLayer.Interfaces;
using Selly.DataLayer.Repositories;
using Selly.Models.Common.Response;
using Selly.Models.Interfaces;

namespace Selly.BusinessLogic.Core.Base
{
    public abstract class BaseSinglePkCore<TRepo, TModel, TEntity> : BaseCore<TRepo, TModel, TEntity>
        where TRepo : BaseSinglePkRepository<TEntity>
        where TEntity : class, ISinglePkDataAccessObject, new()
        where TModel : class, ISinglePkModel, new()
    {
        public static async Task<Response<TModel>> GetAsync(Guid id, IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entities = await repository.GetAsync(id, navigationProperties).ConfigureAwait(false);
                if (entities == null)
                {
                    return ResponseFactory<TModel>.CreateResponse(false, ResponseCode.ErrorNotFound);
                }

                return ResponseFactory<TModel>.CreateResponse(true, ResponseCode.Success, entities.CopyTo<TModel>());
            }
        }

        public static async Task<Response> DeleteAsync(Guid id)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var result = await repository.DeleteAsync(id).ConfigureAwait(false);

                return ResponseFactory.CreateResponse(result, result ? ResponseCode.Success : ResponseCode.ErrorInvalidInput);
            }
        }
    }
}