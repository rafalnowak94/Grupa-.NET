using System;
using Microsoft.Owin;
using Owin;

namespace ShopASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}


