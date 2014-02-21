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
            string[] keys = custom.ToLower().Split(new char[] { ',', ';' });
            string ret = string.Empty;

            foreach (var key in keys)
            {

                if (key == "url")
                    ret = string.Format("url={0};{1}", context.Request.Url.AbsoluteUri, ret);
                
                //any future keys...
            }

            if (!string.IsNullOrWhiteSpace(ret))
                return ret;
            else
                return base.GetVaryByCustomString(context, custom);
        }
	}
}
