using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Owin;
using Autofac;
using StackExchange.Profiling;

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

      using(var step = StackExchange.Profiling.MiniProfiler.StepStatic("In the OWIN middlweare...")) {
        s.nop("from ExampleMiddleware");
        System.Threading.Thread.Sleep(42);
      }

      await Next.Invoke(context);
    }
  }
}