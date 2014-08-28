using Microsoft.AspNet.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutofacOwinMvc.DummyAuth
{
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
      //nope...
      return Task.FromResult(0);
    }

    public void Dispose() { }
  }
}