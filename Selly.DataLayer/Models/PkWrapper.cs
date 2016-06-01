using System;
using System.Collections.Generic;

namespace Selly.DataLayer.Models
{
    public class PkWrapper
    {
        private PkWrapper()
        {
            PrimaryKeys = new List<Guid>();
        }

        public List<Guid> PrimaryKeys { get; }

        public static PkWrapper New(params Guid[] primaryKeys)
        {
            var pkWrapper = new PkWrapper();

            pkWrapper.PrimaryKeys.AddRange(primaryKeys);

            return pkWrapper;
        }
    }
}
