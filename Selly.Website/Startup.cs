using Owin;
using Selly.BusinessLogic.Service;

namespace Selly.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            VatsInitializationService.Instance.InitializeVats();
            MockDataInitializationService.Instance.InitializeMockData();
            CurrencyUpdaterService.Instance.StartUpdaterService();
        }
    }
}