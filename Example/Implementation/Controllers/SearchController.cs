using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Example.Controllers
{
    /// <summary>
    /// Controller for search..
    /// </summary>
    public class SearchController : SearchBaseController
    {
		public SearchController(ISiteRepository rep)
			: base (rep)
        {
        }

        public override ActionResult Index(RenderModel model) //default Umbraco route
        {
            return Search(model, 1, 10, "");
        }

		public override ActionResult Search(RenderModel model, int p = 1, int s = 10, string q = "")
		{
			return base.Search(model, p, s, q);
		}
    }
}