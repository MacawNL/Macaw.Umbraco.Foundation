using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation
{
	public class Application : UmbracoApplication
	{
		public override string GetVaryByCustomString(HttpContext context, string custom)
		{
			if (custom.ToLower() == "url")
				return context.Request.Url.AbsoluteUri;

			return base.GetVaryByCustomString(context, custom);
		}
	}
}
