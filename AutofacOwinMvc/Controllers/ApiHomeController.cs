using AutofacOwinMvc.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutofacOwinMvc.Controllers
{
  public class ApiHomeController : ApiController
  {
    private ExampleService s;
    public ApiHomeController(ExampleService s) {
      this.s = s;
    }

    public string Get() {
      s.nop("from api controller");
      return "hai";
    }
  }
}
