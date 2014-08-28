using AutofacOwinMvc.DummyAuth;
using AutofacOwinMvc.Example;
using Microsoft.AspNet.Identity;
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


    public ActionResult SignIn() {
      var auth = HttpContext.GetOwinContext().Authentication;
      auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

      var dummy = new DummyUser { Id = 1, UserName = "Dummy1" };

      var identFactory = new Microsoft.AspNet.Identity.ClaimsIdentityFactory<DummyUser, int>();
      var createIdentityTask = identFactory.CreateAsync(new DummyUserManager(), dummy, DefaultAuthenticationTypes.ApplicationCookie);
      createIdentityTask.Wait();

      auth.SignIn(createIdentityTask.Result);

      return RedirectToAction("Auth");
    }


    public ActionResult SignOut() {
      var auth = HttpContext.GetOwinContext().Authentication;
      auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

      return RedirectToAction("Index");
    }


    [Authorize]
    public ActionResult Auth() {
      return View();
    }
  }
}