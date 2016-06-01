using System.Threading;
using Owin;
using Selly.BusinessLogic.Service;

namespace Selly.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            VatsInitializationService.Instance.InitializeVats();
            CurrencyUpdaterService.Instance.StartUpdaterService();

            Thread.Sleep(2000);
            MockDataInitializationService.Instance.InitializeMockData();
        }
    }
}