using Microsoft.Owin.Security.Cookies;
using Owin;
using Autofac.Integration.Owin;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin;
using StackExchange.Profiling;

namespace AutofacOwinMvc
{
  public partial class Startup
  {
    private void configureAuth(IAppBuilder app) {
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        CookieName = DefaultAuthenticationTypes.ApplicationCookie,
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
        Provider = new CookieAuthenticationProvider
        {
          OnValidateIdentity = ctx => {
            var ret = Task.Run(() => {

              System.Diagnostics.Trace.WriteLine("MiniProfiler.Current in OnValidateIdentity:" + ((MiniProfiler.Current == null) ? "null" : "exists"));

              using(var step = MiniProfiler.StepStatic("@OnValidateIdentity")) {

                var ex = ctx.OwinContext.GetAutofacLifetimeScope().Resolve<AutofacOwinMvc.Example.ExampleService>();
                ex.nop("@OnValidateIdentity");

                Task.Delay(42).Wait();

              }
            });
            return ret;
          }
        }
      });
    }
  }
}