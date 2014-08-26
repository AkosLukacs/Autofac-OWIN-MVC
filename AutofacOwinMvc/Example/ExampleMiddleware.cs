using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Owin;
using Autofac;

namespace AutofacOwinMvc.Example
{
  public class ExampleMiddleware : OwinMiddleware
  {
    public ExampleMiddleware(OwinMiddleware next)
      : base(next) {
      System.Diagnostics.Trace.WriteLine("ExampleMiddleware ctor");
    }

    public override async System.Threading.Tasks.Task Invoke(IOwinContext context) {
      var s = context.GetAutofacLifetimeScope().Resolve<ExampleService>();

      s.nop("from ExampleMiddleware");

      await Next.Invoke(context);
    }
  }
}