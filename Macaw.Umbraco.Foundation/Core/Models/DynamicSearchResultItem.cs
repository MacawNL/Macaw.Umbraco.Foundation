using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	/// <summary>
	/// Result item contains highlightext
	/// </summary>
    public class DynamicSearchResultItem : DynamicModel //todo: find another way to accomplish this..
    {

        public DynamicSearchResultItem(IPublishedContent source, ISiteRepository repository)
            : base(source, repository)
        {
        }

        public string HighlightText { get; set; }
    }
}
