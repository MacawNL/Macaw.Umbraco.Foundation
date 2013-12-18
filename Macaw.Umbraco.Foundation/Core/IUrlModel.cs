using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Core
{
	public interface IUrlModel: INullModel
	{
		string Title { get; set; }

		string Url { get; set; }

		bool NewWindow { get; set; }
	}
}
