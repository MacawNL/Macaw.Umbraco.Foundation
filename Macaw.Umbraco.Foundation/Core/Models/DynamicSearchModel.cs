using Examine;
using Examine.LuceneEngine.Providers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using System.Dynamic;

namespace Macaw.Umbraco.Foundation.Core.Models
{

    public class DynamicSearchModel : DynamicCollectionModel //DynamicModel, IPager
    {
        public string Query { get; private set; }

        public DynamicSearchModel(
            IPublishedContent source,
            ISiteRepository repository,
            string query)
			: base(source, repository, repository.Find(string.IsNullOrWhiteSpace(query) ? "<NOT>" : query))

        {
            Repository = repository;
            Query = string.IsNullOrWhiteSpace(query) ? "<NOT>" : query;
        }
    }
}