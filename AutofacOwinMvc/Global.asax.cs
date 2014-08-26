using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace AutofacOwinMvc
{
  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e) {

      GlobalConfiguration.Configure(config => {
        config.Routes.MapHttpRoute(
                                name: "DefaultApi",
                                routeTemplate: "api/{controller}/{id}",
                                defaults: new { id = RouteParameter.Optional }
                                );
      });

      System.Web.Routing.RouteTable.Routes.MapRoute(
                                name: "Default",
                                url: "{controller}/{action}/{id}",
                                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                                );

    }

    protected void Session_Start(object sender, EventArgs e) {

    }

    protected void Application_BeginRequest(object sender, EventArgs e) {

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e) {

    }

    protected void Application_Error(object sender, EventArgs e) {

    }

    protected void Session_End(object sender, EventArgs e) {

    }

    protected void Application_End(object sender, EventArgs e) {

    }
  }
}