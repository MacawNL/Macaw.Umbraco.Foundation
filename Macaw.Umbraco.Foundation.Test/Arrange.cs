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
        public static Mock<IPublishedProperty> Property(string alias, object value)
        {
			var mockedProp = new Moq.Mock<IPublishedProperty>(MockBehavior.Strict);
            mockedProp.SetupGet(m => m.PropertyTypeAlias).Returns(alias);
            mockedProp.SetupGet(m => m.Value).Returns(value);
			//mockedProp.SetupGet(m => m.DataValue).Returns(value);
            mockedProp.SetupGet(m => m.HasValue).Returns(true);

            return mockedProp;
        }

		public static Mock<IPublishedContent> Content()
		{
			return Content("Lorem ipsum dolor");
		}

        public static Mock<IPublishedContent> Content(string name, bool umbracoNaviHide = false)
		{
			return Content(name, new List<IPublishedContent>(), umbracoNaviHide);
		}

        public static Mock<IPublishedContent> Content(string name, IEnumerable<IPublishedContent> children, bool umbracoNaviHide = false)
		{
			var mockedItem = new Moq.Mock<IPublishedContent>();
			mockedItem.SetupGet(m => m.Id).Returns(1);
			mockedItem.SetupGet(m => m.Name).Returns(name);
			mockedItem.SetupGet(m => m.Children).Returns(children);

            //define properties
            var props = new List<IPublishedProperty>() 
                {
                    Property("title", name).Object,
                    Property("mainBody", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam gravida vehicula eleifend. Aenean dapibus ligula nisl, eget faucibus ligula vehicula non. Nullam pellentesque rhoncus rhoncus. Donec at ipsum mi. Phasellus eget augue eu lectus placerat lacinia. Sed justo libero, facilisis vitae lectus ut, venenatis interdum dui. In at tincidunt arcu, sit amet egestas elit. Vestibulum ac scelerisque augue. Aenean ut sagittis lacus, in aliquam nisi. Etiam ac massa nec purus malesuada sodales et sed neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae").Object,
                    Property(Umbraco.Core.Constants.Conventions.Content.NaviHide, umbracoNaviHide).Object
                };

                //set properties in property collection
            mockedItem.SetupGet(m => m.Properties).Returns(props);

                //mock GetProperty function aswell
            foreach (var prop in props)
            {
				mockedItem.Setup(m => m.GetProperty(prop.PropertyTypeAlias))
					.Returns(prop);

                mockedItem.Setup(m => m.GetProperty(prop.PropertyTypeAlias, It.IsAny<bool>()))
                    .Returns(prop);
            }

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
