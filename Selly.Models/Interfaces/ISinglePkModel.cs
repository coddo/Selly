using System;

namespace Selly.Models.Interfaces
{
    public interface ISinglePkModel : IModel
    {
        Guid Id { get; set; }
    }
}
