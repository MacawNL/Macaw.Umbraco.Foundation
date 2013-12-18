using Autofac;
using Autofac.Integration.Mvc;
using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Events;
using Macaw.Umbraco.Foundation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;
using Example.DependencyResolution;

namespace Example.Events
{
	public class StartupHandler : FoundationEventHandler
	{
		public override void InitializeAtStartup(
			Umbraco.Core.UmbracoApplicationBase umbracoApplication, 
			Umbraco.Core.ApplicationContext applicationContext, 
			out Func<IServiceLocator> locator, out System.Web.Mvc.IDependencyResolver resolver)
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(Macaw.Umbraco.Foundation.Controllers.DynamicBaseController).Assembly);
			builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());

			builder.Register(s => new SiteRepository(
				applicationContext.Services.ContentService, 
				new UmbracoHelper(UmbracoContext.Current)))
					.As<ISiteRepository>()
					.InstancePerHttpRequest();

			var container = builder.Build();
			resolver = new Autofac.Integration.Mvc.AutofacDependencyResolver(container);
			locator = () => new AutofacServiceLocator();
		}
	}
}