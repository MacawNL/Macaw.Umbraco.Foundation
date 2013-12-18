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

namespace Website.Test
{
	[TestClass]
    public class SearchControllerTest // Naming convention: Method_to_test__State_under_test__Expected_behavior
	{
		[TestMethod]
		public void Search__NoResultsInRepository__Total_results_equals_ResultsCount()
		{
			//1. Arrange
			var mSite = new Mock<ISiteRepository>();
			mSite.Setup(s => s.Find(It.IsAny<string>()))
				.Returns(new List<DynamicModel>()); //mocked object

			var mController = new Mock<SearchBaseController>(mSite.Object) { CallBase = true }; //abstract class callBase
			var renderModel = new RenderModel(Arrange.Content().Object, CultureInfo.InvariantCulture);

			//2.Act
			var result = mController.Object.Search(renderModel, 
                p:1,
                q: "lorem") as ViewResult;

			//3. Assert.
            Assert.IsTrue((result.Model as DynamicSearchModel).Results.Count() == 0, "Results are not '0' when no items are returned.");
            Assert.AreEqual((result.Model as DynamicSearchModel).TotalResults, (result.Model as DynamicSearchModel).Results.Count());
            Assert.IsTrue((result.Model as DynamicSearchModel).Query == "lorem" , "Returned Query is not equal to given search query");

		}

        [TestMethod]
        public void Search__EmptyQuery__Returns_NOT_As_Query()
        {
            //1. Arrange
			var mSite = new Mock<ISiteRepository>();
			mSite.Setup(s => s.Find(It.IsAny<string>()))
				.Returns(new List<DynamicModel>());

			var mController = new Mock<SearchBaseController>(mSite.Object) { CallBase = true }; //abstract class callBase
			var renderModel = new RenderModel(Arrange.Content().Object, CultureInfo.InvariantCulture);

            //2.Act
            var result = mController.Object.Search(renderModel,
                p: 1,
                q: "") as ViewResult;

            //3. Assert.
            Assert.IsTrue((result.Model as DynamicSearchModel).Query == "<NOT>", "The <NOT> query syntax is not used for an empty search query");
        }

        [TestMethod]
        public void Search_Pagesize3_ItemCount5_DisplayPage2__Page2_Returns_2_Items()
        {
			//1. Arrange
			var mSite = new Mock<ISiteRepository>();
			mSite.Setup(s => s.Find(It.IsAny<string>()))
				.Returns(Arrange.BasicPages(mSite.Object).Take(5)); //mocked object

			var mController = new Mock<SearchBaseController>(mSite.Object) { CallBase = true }; //abstract class callBase
			var renderModel = new RenderModel(Arrange.Content().Object, CultureInfo.InvariantCulture);

			//2.Act
			var result = mController.Object.Search(renderModel,
				p: 2,
				s: 3,
				q: "search query") as ViewResult;

			//3. Assert.
			Assert.IsTrue((result.Model as DynamicSearchModel).TotalResults == 5, "Total results does not contain 5 items");
			Assert.IsTrue((result.Model as DynamicSearchModel).Results.Count() == 2, "Resultset does not contain the correct amount of items");
        }
	}
}
