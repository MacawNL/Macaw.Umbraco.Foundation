using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umbraco.Web.Models;
using Umbraco.Core.Models;
using Macaw.Umbraco.Foundation.Core.Models;
using Moq;
using System.Collections.Generic;
using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace Macaw.Umbraco.Foundation.Test
{
	[TestClass]
    public class ContainerBaseControllerTest : BaseTestClass // Naming convention: Method_to_test__State_under_test__Expected_behavior
	{
		[TestMethod]
		public void Container__NoChilds__Total_results_equals_ResultsCount()
		{
			//1. Arrange
			var mSite = new Mock<ISiteRepository>();

			var mController = new Mock<ContainerBaseController>(mSite.Object) { CallBase = true }; //abstract class callBase
			var renderModel = new RenderModel(Arrange.Content().Object, CultureInfo.InvariantCulture);

			//2.Act
			var result = mController.Object.Container(renderModel, 
                p:1) as ViewResult;

			//3. Assert.
            Assert.IsTrue((result.Model as DynamicCollectionModel).Results.Count() == 0, "Results are not '0' when no items are returned.");
			Assert.AreEqual((result.Model as DynamicCollectionModel).TotalResults, (result.Model as DynamicCollectionModel).Results.Count());
		}

        [TestMethod]
        public void Container__5Childs_PageSize4__Page2_Returns_1_Item()
        {
			//1. Arrange
			var mSite = new Mock<ISiteRepository>();

			var mController = new Mock<ContainerBaseController>(mSite.Object) { CallBase = true }; //abstract class callBase
			var mContent = Arrange.Content("lorem parent page",
			new List<IPublishedContent>()
				{
					{ Arrange.Content("Lorem child page 1").Object },
					{ Arrange.Content("Ipsum child page 2").Object },
					{ Arrange.Content("Dolor child page 3").Object },
					{ Arrange.Content("Sit child page 4").Object },
					{ Arrange.Content("Consectetur child page 5").Object }
				});

			var renderModel = new RenderModel(mContent.Object, CultureInfo.InvariantCulture);

			//2.Act
			var result = mController.Object.Container(renderModel,
				p: 2,
				s: 4) as ViewResult;

			//3. Assert.
			Assert.IsTrue((result.Model as DynamicCollectionModel).TotalResults == 5, "Total results does not contain 5 items");
			Assert.IsTrue((result.Model as DynamicCollectionModel).Results.Count() == 1, "Resultset for page 2 does not contain the correct amount of items");
        }
	}
}
