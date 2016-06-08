using System.Threading.Tasks;
using Selly.BusinessLogic.Core;
using Selly.Models;
using System;

namespace Selly.BusinessLogic.Service
{
    public class VatsInitializationService
    {
        private readonly ValueAddedTax[] mValueAddedTaxes;

        private static VatsInitializationService mInstance;

        private VatsInitializationService()
        {
            mValueAddedTaxes = new[]
            {
                new ValueAddedTax {Value = 20, Id = Guid.Parse("B3312938-75E0-4711-AF84-A66750724EB1")},
                new ValueAddedTax {Value = 9, Id = Guid.Parse("0BE69C70-ADF1-4321-B022-20583F207526")},
                new ValueAddedTax {Value = 5, Id = Guid.Parse("3F659D46-9AF8-4E7D-8A0C-D6EDF1218BB9")}
            };
        }

        public static VatsInitializationService Instance => mInstance ?? (mInstance = new VatsInitializationService());

        public void InitializeVats()
        {
            Task.Run(async () =>
            {
                var existingVats = await VatCore.GetAllAsync().ConfigureAwait(false);
                if (existingVats.Data != null && existingVats.Data.Count != 0)
                {
                    return;
                }

                await VatCore.CreateAsync(mValueAddedTaxes).ConfigureAwait(false);
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}