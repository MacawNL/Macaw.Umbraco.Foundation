using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Controllers
{
	public class ListingController : ContainerBaseController
	{
		public ListingController(ISiteRepository rep)
			: base (rep)
        {
        }

		public override System.Web.Mvc.ActionResult Index(Umbraco.Web.Models.RenderModel model)
		{
			return Container(model, 1, 10);
		}

		public System.Web.Mvc.ActionResult Listing(Umbraco.Web.Models.RenderModel model, int p = 1, int s = 10)
		{
			return Container(model, p, s);
		}
	}
}