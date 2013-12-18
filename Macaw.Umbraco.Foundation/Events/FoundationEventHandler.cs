using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Macaw.Umbraco.Foundation.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Configuration;
using System.IO;
using Umbraco.Core.Models;
using Macaw.Umbraco.Foundation.Infrastructure;
using Macaw.Umbraco.Foundation.Core;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Events
{
	public abstract class FoundationEventHandler : IApplicationEventHandler
	{
		public virtual void RegisterBaseRoutes()
		{
			//todo: add route for json and rss.
			RouteTable.Routes.MapRoute(
			"Sitemap",
			"sitemap",
			new
			{
				controller = "DynamicBase",
				action = "sitemap",
				id = UrlParameter.Optional
			}); 
		}

		public virtual void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			//nothing
		}

		public virtual void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			//nothing
		}

		public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			Func<IServiceLocator> locator;
			IDependencyResolver resolver;
			InitializeAtStartup(umbracoApplication, applicationContext,
				out locator, out resolver);

			ServiceLocator.SetServiceLocator(locator);
			DependencyResolver.SetResolver(resolver);

			RegisterBaseRoutes();
			DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DynamicBaseController));
		}

		public abstract void InitializeAtStartup(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext,
			out Func<IServiceLocator> locator, out IDependencyResolver resolver);
	}
}