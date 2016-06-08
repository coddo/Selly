using System;

namespace Selly.DataLayer.Interfaces
{
    public interface ISinglePkDataAccessObject : IDataAccessObject
    {
        Guid Id { get; set; }
    }
}
