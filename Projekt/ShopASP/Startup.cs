using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopASP.Startup))]
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


