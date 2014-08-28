using AutofacOwinMvc.Example;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class DummyUser : IUser<int>
    {
      public int Id { get; set; }
      public string UserName { get; set; }
    }

    public class DummyUserStore : IUserStore<DummyUser, int>
    {
      private static ConcurrentDictionary<int, DummyUser> __userStore = new ConcurrentDictionary<int, DummyUser>();

      public System.Threading.Tasks.Task CreateAsync(DummyUser user) {
        __userStore.TryAdd(user.Id, user);

        return Task.FromResult(0);
      }

      public System.Threading.Tasks.Task DeleteAsync(DummyUser user) {
        DummyUser junk;
        __userStore.TryRemove(user.Id, out junk);

        return Task.FromResult(0);
      }

      public System.Threading.Tasks.Task<DummyUser> FindByIdAsync(int userId) {
        DummyUser foundUser;
        __userStore.TryGetValue(userId, out foundUser);

        return Task.FromResult(foundUser);
      }

      public System.Threading.Tasks.Task<DummyUser> FindByNameAsync(string userName) {
        var user = __userStore.FirstOrDefault(x => x.Value.UserName == userName);

        return Task.FromResult(user.Value);
      }

      public System.Threading.Tasks.Task UpdateAsync(DummyUser user) {
        return Task.FromResult(0);
      }

      public void Dispose() { }
    }

    public class DummyUserManager : UserManager<DummyUser, int>
    {
      public DummyUserManager()
        : base(new DummyUserStore()) {

      }
    }

    [Authorize]
    public ActionResult Auth() {
      return View();
    }
  }
}