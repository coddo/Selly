using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Selly.Website.Startup))]

namespace Selly.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
