using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacOwinMvc.DummyAuth
{
  public class DummyUser : IUser<int>
  {
    public int Id { get; set; }
    public string UserName { get; set; }
  }
}