using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	/// <summary>
	/// Result item contains highlightext, used by the repository
	/// </summary>
    public class DynamicSearchResultItem : DynamicModel 
    {

        public DynamicSearchResultItem(IPublishedContent source, ISiteRepository repository)
            : base(source, repository)
        {
        }

        public string HighlightText { get; set; }
    }
}
