using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutofacOwinMvc.Example
{
  public class ExampleService : IDisposable
  {
    private Guid id;

    public ExampleService() {
      this.id = Guid.NewGuid();
      Trace.WriteLine("ExampleClass ctor " + id.ToString());

    }

    public void nop(string msg) {
      Trace.WriteLine(String.Format("ExampleService '{0}', msg: {1}", id, msg));
    }

    public void Dispose() {
      Trace.WriteLine(String.Format("Disposing ExampleService '{0}'", id));
    }
  }
}