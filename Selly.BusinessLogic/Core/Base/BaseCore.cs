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
    public abstract class BaseCore<TRepo, TModel, TEntity>
        where TRepo : BaseRepository<TEntity>
        where TEntity : class, IDataAccessObject, new()
        where TModel : class, IModel, new()
    {
        public static async Task<Response<IList<TModel>>> GetAllAsync(IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entities = await repository.GetAllAsync(navigationProperties).ConfigureAwait(false);
                if (entities == null)
                {
                    ResponseFactory<IList<TModel>>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<IList<TModel>>.CreateResponse(true, ResponseCode.Success, entities.CopyTo<TModel>());
            }
        }

        public static async Task<Response<TModel>> CreateAsync(TModel model, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entity = model.CopyTo<TEntity>();

                entity = await repository.CreateAsync(entity, refreshFromDb, navigationProperties).ConfigureAwait(false);
                if (entity == null)
                {
                    return ResponseFactory<TModel>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                return ResponseFactory<TModel>.CreateResponse(true, ResponseCode.Success, entity.CopyTo<TModel>());
            }
        }

        public static async Task<Response<IList<TModel>>> CreateAsync(IList<TModel> modelCollection, bool refreshFromDb = false,
            IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                entityCollection = await repository.CreateAsync(entityCollection, refreshFromDb, navigationProperties).ConfigureAwait(false);
                if (entityCollection == null)
                {
                    return ResponseFactory<IList<TModel>>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                return ResponseFactory<IList<TModel>>.CreateResponse(true, ResponseCode.Success, entityCollection.CopyTo<TModel>());
            }
        }

        public static async Task<Response<TModel>> UpdateAsync(TModel model, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entity = model.CopyTo<TEntity>();

                entity = await repository.UpdateAsync(entity, refreshFromDb, navigationProperties).ConfigureAwait(false);
                if (entity == null)
                {
                    return ResponseFactory<TModel>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                return ResponseFactory<TModel>.CreateResponse(true, ResponseCode.Success, entity.CopyTo<TModel>());
            }
        }

        public static async Task<Response<IList<TModel>>> UpdateAsync(IList<TModel> modelCollection, bool refreshFromDb = false,
            IList<string> navigationProperties = null)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                entityCollection = await repository.UpdateAsync(entityCollection, refreshFromDb, navigationProperties).ConfigureAwait(false);
                if (entityCollection == null)
                {
                    return ResponseFactory<IList<TModel>>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                return ResponseFactory<IList<TModel>>.CreateResponse(true, ResponseCode.Success, entityCollection.CopyTo<TModel>());
            }
        }

        public static async Task<Response> DeleteAsync(TModel model)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entity = model.CopyTo<TEntity>();

                var result = await repository.DeleteAsync(entity).ConfigureAwait(false);
                return ResponseFactory.CreateResponse(result, result ? ResponseCode.Success : ResponseCode.ErrorInvalidInput);
            }
        }

        public static async Task<Response> DeleteAsync(IList<TModel> modelCollection)
        {
            using (var repository = DataLayerUnitOfWork.Repository<TRepo>())
            {
                var entityCollection = modelCollection.CopyTo<TEntity>();

                var result = await repository.DeleteAsync(entityCollection).ConfigureAwait(false);
                return ResponseFactory.CreateResponse(result, result ? ResponseCode.Success : ResponseCode.ErrorInvalidInput);
            }
        }
    }
}