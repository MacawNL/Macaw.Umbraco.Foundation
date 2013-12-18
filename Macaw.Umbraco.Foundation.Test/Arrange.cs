using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Models;


namespace Website.Test
{
	/// <summary>
	/// Arrange helper class, contains some default mocks and datasources.
	/// </summary>
	public class Arrange
	{
		public static Mock<IPublishedContent> Content()
		{
			return Content("Lorem ipsum dolor");
		}

		public static Mock<IPublishedContent> Content(string name)
		{
			return Content(name, new List<IPublishedContent>());
		}

		public static Mock<IPublishedContent> Content(string name, IEnumerable<IPublishedContent> children)
		{
			var mockedItem = new Moq.Mock<IPublishedContent>();
			mockedItem.SetupGet(m => m.Id).Returns(1);
			mockedItem.SetupGet(m => m.Name).Returns(name);
			mockedItem.SetupGet(m => m.Children).Returns(children);

			return mockedItem;
		}

		public static List<DynamicModel> BasicPages(ISiteRepository repository)
		{
			List<DynamicModel> ret = new List<DynamicModel>()
			{
				{ new DynamicModel(Content("Lorem page 1").Object, repository) },
				{ new DynamicModel(Content("Ipsum page 2").Object, repository) },
				{ new DynamicModel(Content("Dolor page 3").Object, repository) },
				{ new DynamicModel(Content("Sit page 4").Object, repository) },
				{ new DynamicModel(Content("Consectetur page 5").Object, repository) },
				{ new DynamicModel(Content("Donec page 6").Object, repository) },
				{ new DynamicModel(Content("Commodo page 7").Object, repository) }
			};

			return ret;
		}
	}
}
