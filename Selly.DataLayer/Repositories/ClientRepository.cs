using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Selly.DataLayer.Repositories.Base;

namespace Selly.DataLayer.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        protected override Expression<Func<Client, bool>> GetFindByIdQuery(IList<Guid> primaryKeys)
        {
            if (primaryKeys == null || primaryKeys.Count != 2)
            {
                return null;
            }

            return entity => entity.Id == primaryKeys[0] && entity.CurrencyId == primaryKeys[1];
        }

        protected override bool ValidateEntity(Client entity)
        {
            return entity != null && entity.Id != Guid.Empty && entity.CurrencyId != Guid.Empty;
        }
    }
}