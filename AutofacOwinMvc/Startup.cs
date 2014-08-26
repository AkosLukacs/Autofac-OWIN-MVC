using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using AutofacOwinMvc.Example;
using System.Web.Http;

[assembly: OwinStartup(typeof(AutofacOwinMvc.Startup))]

namespace AutofacOwinMvc
{
  public class Startup
  {
    public void Configuration(IAppBuilder app) {
      

      var builder = new ContainerBuilder();



      builder.RegisterControllers(Assembly.GetExecutingAssembly());
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      builder.RegisterType<ExampleService>().InstancePerRequest();
      builder.RegisterType<ExampleMiddleware>().InstancePerRequest();





      var container = builder.Build();

      System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      app.UseAutofacMvc();
      app.UseAutofacMiddleware(container);
      app.UseAutofacWebApi(GlobalConfiguration.Configuration);
    }
  }
}