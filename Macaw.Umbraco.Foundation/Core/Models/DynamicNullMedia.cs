using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Umbraco.Core.Dynamics;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	public class DynamicNullMedia : DynamicNull
	{
		public DynamicNullMedia()
			: base()
		{
		}

		public string Url
		{
			get
			{
				var ret = WebConfigurationManager.AppSettings["Macaw.Umbraco.Foundation.EmptyImageUrl"];
				return ret == null ? string.Empty : ret;
			}
		}
	}
}
