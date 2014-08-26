using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutofacOwinMvc.Example;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AutofacOwinMvc.Startup))]

namespace AutofacOwinMvc
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<ExampleService>().InstancePerRequest();
			builder.RegisterType<ExampleMiddleware>().InstancePerRequest();
			var container = builder.Build();

			// When using OWIN, you don't use GlobalConfiguration - you create
			// an HttpConfiguration and set it up independently.
			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
									name: "DefaultApi",
									routeTemplate: "api/{controller}/{id}",
									defaults: new { id = RouteParameter.Optional }
									);

			// I didn't see anywhere that the WebAPI dependency resolver was getting
			// set - don't forget to do this.
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			// Set the MVC dependency resolver as well.
			System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			// OWIN middleware *order* is very important. You're setting up a
			// pipeline and first registered is first to execute. The order was
			// previously wrong, and the "app.UseWebApi(config)" was missing,
			// so WebAPI didn't know it was supposed to run through OWIN.
			app.UseAutofacMiddleware(container);
			app.UseAutofacWebApi(config);
			app.UseAutofacMvc();
			app.UseWebApi(config);
		}
	}
}