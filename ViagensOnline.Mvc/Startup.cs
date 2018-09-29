using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(ViagensOnline.Mvc.Startup))]

namespace ViagensOnline.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions()
                {
                    AuthenticationType = "ViagensOnlineCookieAuth",
                    LoginPath = new PathString("/Admin/Login")                    
                }
                );
        }
    }
}
