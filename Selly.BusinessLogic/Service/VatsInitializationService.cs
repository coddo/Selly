using System.Threading.Tasks;
using Selly.BusinessLogic.Core;
using Selly.Models;

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
                new ValueAddedTax
                {
                    Value = 20
                },
                new ValueAddedTax
                {
                    Value = 9
                },
                new ValueAddedTax
                {
                    Value = 5
                }
            };
        }

        public static VatsInitializationService Instance => mInstance ?? (mInstance = new VatsInitializationService());

        public void InitializeVats()
        {
            Task.Run(async () =>
            {
                var existingVats = await VatCore.GetAllAsync().ConfigureAwait(false);
                if (existingVats?.Count != 0)
                {
                    return;
                }

                await VatCore.CreateAsync(mValueAddedTaxes).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}