using Examine;
using Examine.LuceneEngine.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Core.Dynamics;
using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;

namespace Macaw.Umbraco.Foundation.Controllers
{
    /// <summary>
	/// Default Container Controller..
    /// </summary>
	public abstract class ContainerBaseController : DynamicBaseController
    {
		public ContainerBaseController(ISiteRepository rep)
			: base (rep)
        {
        }

		public override abstract ActionResult Index(RenderModel model);

		/// <summary>
		/// This default implementation is using the childcollection as the container..
        /// Allowed querystring parameters:
        /// p = Pagenumber
        /// s = pageSize
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
		public virtual ActionResult Container(RenderModel model,
			int p=1, //read querystring parameters without using this.Request.
            int s=10)
		{
			var resultModel = new DynamicCollectionModel(model.Content, Repository, model.Content.Children);

			resultModel.PagedResults = () =>
			{
				return model.Content.Children
					.Skip(s * (p - 1))
					.Take(s)
					.Select(n => new DynamicModel(n, Repository));
			};

			return View(resultModel);
		}
    }
}