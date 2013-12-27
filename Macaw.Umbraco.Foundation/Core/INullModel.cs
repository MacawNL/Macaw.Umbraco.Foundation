using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Core
{
	public interface INullModel
	{
		/// <summary>
		/// available for all dynamic objects, using this construction prevents 
		/// nullreference exceptions on non existing properties
		/// </summary>
		/// <returns></returns>
		bool IsNull();

		//todo: HasValue() ??
	}
}
