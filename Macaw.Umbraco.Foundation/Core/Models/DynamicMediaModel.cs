using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	public class DynamicMediaModel : DynamicModel
	{
		private string _url;

		public DynamicMediaModel(IPublishedContent source, ISiteRepository repository)
			: base(source, repository)
		{
			_url = source.Url;
		}

		public override string Url
		{
			get
			{
				return _url;
			}
		}
	}
}
