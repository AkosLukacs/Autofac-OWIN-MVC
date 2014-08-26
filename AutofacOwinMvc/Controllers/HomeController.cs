using AutofacOwinMvc.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutofacOwinMvc.Controllers
{
  public class HomeController : Controller
  {
    private ExampleService s;
    public HomeController(ExampleService s) {
      this.s = s;
    }


    // GET: Home
    public ActionResult Index() {

      s.nop("from HomeController");

      return View();
    }
  }
}