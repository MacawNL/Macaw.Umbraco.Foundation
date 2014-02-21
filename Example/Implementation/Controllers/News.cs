using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Controllers
{
	public class NewsController : DynamicBaseController //DocumentType
	{
        public NewsController(ISiteRepository rep)
			: base (rep)
        {
        }

		public override System.Web.Mvc.ActionResult Index(Umbraco.Web.Models.RenderModel model) //base
		{
            return base.Index(model);
		}

        public System.Web.Mvc.ActionResult NewsOld(Umbraco.Web.Models.RenderModel model) //template name
        {
            //default umbraco
            var template = ControllerContext.RouteData.Values["action"].ToString();
            return View(template, model);
        }
	}
}