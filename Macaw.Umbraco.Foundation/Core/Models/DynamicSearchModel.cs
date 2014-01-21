using Examine;
using Examine.LuceneEngine.Providers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using System.Dynamic;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	/// <summary>
	/// Search "view" model used by the searchcontroller
	/// </summary>
    public class DynamicSearchModel : DynamicCollectionModel 
    {
        public string Query { get; private set; }

        public DynamicSearchModel(
            IPublishedContent source,
            ISiteRepository repository,
            string query)
			: base(source, repository, 
				repository.Find(string.IsNullOrWhiteSpace(query) ? "<NOT>" : query)
				.Where(f => f.IsVisible())
			)

        {
            Query = string.IsNullOrWhiteSpace(query) ? "<NOT>" : query;
        }
    }
}