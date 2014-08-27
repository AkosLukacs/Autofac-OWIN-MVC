using System;
using System.Web.Mvc;

namespace AutofacOwinMvc
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			// Moved the HttpConfiguration part out because you set up OWIN
			// routes on the instance HttpConfiguration, not on GlobalConfiguration.
			System.Web.Routing.RouteTable.Routes.MapRoute(
									  name: "Default",
									  url: "{controller}/{action}/{id}",
									  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
									  );

		}
	}
}