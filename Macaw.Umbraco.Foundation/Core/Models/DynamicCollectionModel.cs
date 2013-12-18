using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	public class DynamicCollectionModel : DynamicModel, IPager, IEnumerable<DynamicModel>
	{
		private IEnumerable<DynamicModel> Container { get; set; }

		public DynamicCollectionModel(IPublishedContent source, ISiteRepository repository, IPublishedContent containerParent)
			: base(source, repository)
		{
			Container = containerParent.Children.Select(n => new DynamicModel(n, Repository));
		}

		/// <summary>
		/// Dynamic Model Collection used for list view pages..
		/// </summary>
		/// <param name="source">Published content showing all data.</param>
		/// <param name="container">Container containing all list items</param>
		public DynamicCollectionModel(IPublishedContent source, ISiteRepository repository, IEnumerable<IPublishedContent> container)
			:base(source, repository)
		{
			Container = container.Select(n => new DynamicModel(n, Repository));
		}

		public Func<IEnumerable<DynamicModel>> PagedResults { get; set; }

		public virtual IEnumerable<DynamicModel> Results
		{
			get
			{
				if (PagedResults == null)
					return Container;

				return PagedResults();
			}
		}

		public int TotalResults
		{
			get { return Container.Count(); }
		}

		public IEnumerator<DynamicModel> GetEnumerator()
		{
			return Results.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
