using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacOwinMvc.DummyAuth
{

  public class DummyUserManager : UserManager<DummyUser, int>
  {
    public DummyUserManager()
      : base(new DummyUserStore()) {
    }
  }
}